using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using NSubstitute;
using TfsCmdlets.Cmdlets.Wiki;
using TfsCmdlets.HttpClients;
using TfsCmdlets.Services;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Controllers.Wiki
{
    public class NewWiki_Tests
    {
        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly IWikiHttpClient _client = Substitute.For<IWikiHttpClient>();
        private readonly TeamProject _project = new TeamProject { Id = Guid.NewGuid(), Name = "MyProject" };
        private readonly GitRepository _repo = new GitRepository { Id = Guid.NewGuid(), Name = "MyRepo", DefaultBranch = "refs/heads/main" };
        private WikiCreateParametersV2 _sentParams;

        public NewWiki_Tests()
        {
            _data.GetProject().Returns(_project);
            _data.GetItem<GitRepository>(Arg.Any<object>()).Returns(_repo);
            _powerShell.ShouldProcess(Arg.Any<TeamProject>(), Arg.Any<string>()).Returns(true);
            _parms.Get<string>("Wiki", Arg.Any<string>()).Returns("MyWiki");
            _parms.Get<object>("Repository", Arg.Any<object>()).Returns("MyRepo");
            _parms.Get<string>("Path", Arg.Any<string>()).Returns("/");

            _client.CreateWikiAsync(Arg.Do<WikiCreateParametersV2>(p => _sentParams = p), Arg.Any<object>(), Arg.Any<CancellationToken>())
                .Returns(ci => Task.FromResult(new WikiV2 { Name = ci.Arg<WikiCreateParametersV2>().Name }));
        }

        private NewWikiController CreateController() => new NewWikiController(_powerShell, _data, _parms, _logger, _client);

        [Fact]
        public void Code_Wiki_Uses_Explicit_Branch_And_Path()
        {
            _parms.HasParameter("Branch").Returns(true);
            _parms.Get<string>("Branch", Arg.Any<string>()).Returns("develop");
            _parms.Get<string>("Path", Arg.Any<string>()).Returns("/docs");

            var results = CreateController().InvokeCommand().Cast<object>().ToList();

            Assert.Single(results);
            Assert.Equal(WikiType.CodeWiki, _sentParams.Type);
            Assert.Equal(_repo.Id, _sentParams.RepositoryId);
            Assert.Equal("/docs", _sentParams.MappedPath);
            Assert.Equal("develop", _sentParams.Version.Version);
            Assert.Equal(GitVersionType.Branch, _sentParams.Version.VersionType);
        }

        [Fact]
        public void Code_Wiki_Defaults_To_Repository_Default_Branch()
        {
            var results = CreateController().InvokeCommand().Cast<object>().ToList();

            Assert.Single(results);
            Assert.Equal("main", _sentParams.Version.Version);
            Assert.Equal("/", _sentParams.MappedPath);
        }

        [Fact]
        public void Code_Wiki_Throws_When_No_Branch_Available()
        {
            _repo.DefaultBranch = null;

            Assert.Throws<ArgumentException>(() => CreateController().InvokeCommand().Cast<object>().ToList());
            Assert.Null(_sentParams);
        }

        [Fact]
        public void Does_Not_Create_Wiki_When_ShouldProcess_Returns_False()
        {
            _powerShell.ShouldProcess(Arg.Any<TeamProject>(), Arg.Any<string>()).Returns(false);

            var results = CreateController().InvokeCommand().Cast<object>().ToList();

            Assert.Empty(results);
            Assert.Null(_sentParams);
        }
    }
}
