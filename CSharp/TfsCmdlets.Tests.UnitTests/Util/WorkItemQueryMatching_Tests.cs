using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using NSubstitute;
using TfsCmdlets.Controllers.WorkItem.Query;
using TfsCmdlets.HttpClients;
using TfsCmdlets.Services;
using TfsCmdlets.Services.Impl;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Util
{
    public class WorkItemQueryMatching_Tests
    {
        [Theory]
        [InlineData("Steve's unclosed tickets", true)]
        [InlineData("Steve's*", true)]
        [InlineData("Steve unclosed tickets", false)]
        public void Query_Patterns_Match_Apostrophes_Literally(string pattern, bool expected)
        {
            var query = new QueryHierarchyItem { Name = "Steve's unclosed tickets", Path = "Shared Queries/Steve's unclosed tickets", IsFolder = false };
            var roots = new List<QueryHierarchyItem>
            {
                new QueryHierarchyItem { Name = "My Queries", IsPublic = false, IsFolder = true },
                new QueryHierarchyItem { Name = "Shared Queries", IsPublic = true, IsFolder = true, Children = new List<QueryHierarchyItem> { query } }
            };
            var parameters = Substitute.For<IParameterManager>();
            parameters.Get<string>("ItemType").Returns("Query");
            parameters.Get<object>("Query").Returns(pattern);
            parameters.Get<QueryItemScope>("Scope", QueryItemScope.Both).Returns(QueryItemScope.Both);
            var data = Substitute.For<IDataManager>();
            data.GetProject().Returns(new TeamProject { Name = "Project" });
            var client = Substitute.For<IWorkItemTrackingHttpClient>();
            client.GetQueriesAsync("Project", Arg.Any<QueryExpand?>(), Arg.Any<int?>(), true)
                .Returns(Task.FromResult(roots));
            var controller = new GetWorkItemQueryController(new NodeUtilImpl(), Substitute.For<IPowerShellService>(),
                data, parameters, Substitute.For<ILogger>(), client);

            var result = controller.InvokeCommand().Cast<QueryHierarchyItem>().ToList();

            if (expected) Assert.Same(query, Assert.Single(result));
            else Assert.Empty(result);
        }
    }
}
