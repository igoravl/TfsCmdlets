using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using NSubstitute;
using TfsCmdlets.Cmdlets.Pipeline.Build;
using TfsCmdlets.Cmdlets.RestApi;
using TfsCmdlets.Cmdlets.WorkItem.Linking;
using TfsCmdlets.HttpClients;
using TfsCmdlets.Services;
using Xunit;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

namespace TfsCmdlets.Tests.UnitTests.Controllers
{
    public class ShouldProcess_Tests
    {
        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();

        public ShouldProcess_Tests()
        {
            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(false);
            _powerShell.ShouldProcess(Arg.Any<Models.Connection>(), Arg.Any<string>()).Returns(false);
            _powerShell.ShouldProcess(Arg.Any<WebApiTeamProject>(), Arg.Any<string>()).Returns(false);
        }

        private IRestApiService SetupRestApi(string path, string method)
        {
            var restApi = Substitute.For<IRestApiService>();
            restApi.Url.Returns(new Uri("https://dev.azure.com/org/_apis/projects"));

            _parms.Get<string>("Path", Arg.Any<string>()).Returns(path);
            _parms.Get<string>("Method", Arg.Any<string>()).Returns(method);
            _parms.HasParameter("AsTask").Returns(true);
            _parms.Get<bool>("AsTask", Arg.Any<bool>()).Returns(true);

            return restApi;
        }

        private static void AssertRestApiInvoked(IRestApiService restApi, int count)
            => restApi.ReceivedWithAnyArgs(count).InvokeTemplateAsync(default, default, default, default, default,
                default, default, default, default, default, default, default);

        [Theory]
        [InlineData("/_apis/projects", "POST")]
        [InlineData("/_apis/projects", "delete")]
        [InlineData("POST /_apis/projects", "GET")]
        [InlineData("PATCH https://dev.azure.com/{organization}/_apis/projects", "GET")]
        public void InvokeRestApi_NonSafe_Method_Is_Not_Sent_When_ShouldProcess_Is_False(string path, string method)
        {
            var restApi = SetupRestApi(path, method);
            var controller = new InvokeRestApiController(restApi, _powerShell, _data, _parms, _logger);

            var results = controller.InvokeCommand().Cast<object>().ToList();

            Assert.Empty(results);
            AssertRestApiInvoked(restApi, 0);
        }

        [Theory]
        [InlineData("/_apis/projects", "GET")]
        [InlineData("/_apis/projects", "head")]
        [InlineData("OPTIONS /_apis/projects", "POST")]
        public void InvokeRestApi_Safe_Method_Is_Sent_Without_ShouldProcess(string path, string method)
        {
            var restApi = SetupRestApi(path, method);
            var controller = new InvokeRestApiController(restApi, _powerShell, _data, _parms, _logger);

            controller.InvokeCommand().Cast<object>().ToList();

            AssertRestApiInvoked(restApi, 1);
            _powerShell.DidNotReceiveWithAnyArgs().ShouldProcess(default(Models.Connection), default);
        }

        [Fact]
        public void InvokeRestApi_NonSafe_Method_Is_Sent_When_ShouldProcess_Is_True()
        {
            var restApi = SetupRestApi("/_apis/projects", "POST");
            _powerShell.ShouldProcess(Arg.Any<Models.Connection>(), Arg.Any<string>()).Returns(true);
            var controller = new InvokeRestApiController(restApi, _powerShell, _data, _parms, _logger);

            controller.InvokeCommand().Cast<object>().ToList();

            AssertRestApiInvoked(restApi, 1);
        }

        [Fact]
        public void StartBuild_Does_Not_Queue_When_ShouldProcess_Is_False()
        {
            var client = Substitute.For<IBuildHttpClient>();
            _data.GetItem<BuildDefinitionReference>(Arg.Any<object>()).Returns(new BuildDefinitionReference { Name = "CI" });
            _data.GetProject().Returns(new WebApiTeamProject { Name = "Project" });

            var controller = new StartBuildController(_powerShell, _data, _parms, _logger, client);

            var results = controller.InvokeCommand().Cast<object>().ToList();

            Assert.Empty(results);
            client.DidNotReceiveWithAnyArgs().QueueBuildAsync(default(Microsoft.TeamFoundation.Build.WebApi.Build), default(string), default, default, default, default, default);
        }

        [Fact]
        public void AddWorkItemLink_Does_Not_Update_When_ShouldProcess_Is_False()
        {
            var client = Substitute.For<IWorkItemTrackingHttpClient>();
            var linkTypes = Substitute.For<IKnownWorkItemLinkTypes>();
            _data.GetItem<WebApiWorkItem>(Arg.Any<object>()).Returns(new WebApiWorkItem { Id = 1, Rev = 1, Url = "https://dev.azure.com/org/_apis/wit/workItems/1" });

            var controller = new AddWorkItemLinkController(linkTypes, _powerShell, _data, _parms, _logger, client);

            var results = controller.InvokeCommand()?.Cast<object>().ToList() ?? new List<object>();

            Assert.Empty(results);
            client.DidNotReceiveWithAnyArgs().UpdateWorkItemAsync(default(JsonPatchDocument), default(int), default, default, default, default, default, default);
        }
    }
}
