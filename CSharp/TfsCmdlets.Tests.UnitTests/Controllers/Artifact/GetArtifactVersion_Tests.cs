using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Feed.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using NSubstitute;
using TfsCmdlets.Cmdlets.Artifact;
using TfsCmdlets.HttpClients;
using TfsCmdlets.Services;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Controllers.Artifact
{
    public class GetArtifactVersion_Tests
    {
        private const string FeedId = "my-feed-id";

        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly IFeedHttpClient _client = Substitute.For<IFeedHttpClient>();
        private readonly Package _package;

        public GetArtifactVersion_Tests()
        {
            var links = new ReferenceLinks();
            links.AddLink("feed", $"https://feeds.dev.azure.com/org/_apis/packaging/Feeds/{FeedId}");

            _package = new Package { Id = Guid.NewGuid(), Name = "MyPackage", Links = links };

            _parms.Get<object>("Artifact", Arg.Any<object>()).Returns(_package);
            _data.GetItem<Package>(Arg.Any<object>()).Returns(_package);
            _data.GetItem<Feed>(Arg.Any<object>()).Returns(new Feed { Name = "MyFeed" });
        }

        private GetArtifactVersionController CreateController(object version, bool includeDeleted = false, bool includeDelisted = false)
        {
            _parms.HasParameter("Version").Returns(true);
            _parms.Get<object>("Version", Arg.Any<object>()).Returns(version);
            _parms.Get<bool>("IncludeDeleted", Arg.Any<bool>()).Returns(includeDeleted);
            _parms.Get<bool>("IncludeDelisted", Arg.Any<bool>()).Returns(includeDelisted);
            return new GetArtifactVersionController(_powerShell, _data, _parms, _logger, _client);
        }

        private void SetupVersions(params PackageVersion[] versions)
        {
            _client.GetPackageVersionsAsync(
                    Arg.Any<string>(), FeedId, _package.Id.ToString(),
                    Arg.Any<bool?>(), Arg.Any<bool?>(), Arg.Any<bool?>(),
                    Arg.Any<object>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(versions.ToList()));
        }

        private static List<object> Flatten(IEnumerable<object> results)
            => results.SelectMany(r => r is System.Collections.IEnumerable e ? e.Cast<object>() : new[] { r }).ToList();

        [Fact]
        public void Exact_Version_Returns_Only_Matching_Version()
        {
            var v1 = new PackageVersion { Id = Guid.NewGuid(), Version = "1.0.0", NormalizedVersion = "1.0.0" };
            var v2 = new PackageVersion { Id = Guid.NewGuid(), Version = "1.0.0-beta", NormalizedVersion = "1.0.0-beta" };
            SetupVersions(v1, v2);

            var results = Flatten(CreateController("1.0.0").InvokeCommand().Cast<object>());

            Assert.Single(results);
            Assert.Same(v1, results[0]);
        }

        [Fact]
        public void Exact_Version_Matches_Normalized_Version_Case_Insensitively()
        {
            var v1 = new PackageVersion { Id = Guid.NewGuid(), Version = "1.0-RC", NormalizedVersion = "1.0.0-rc" };
            SetupVersions(v1);

            var results = Flatten(CreateController("1.0.0-RC").InvokeCommand().Cast<object>());

            Assert.Single(results);
            Assert.Same(v1, results[0]);
        }

        [Fact]
        public void Exact_Version_Not_Found_Logs_Error()
        {
            SetupVersions(new PackageVersion { Id = Guid.NewGuid(), Version = "2.0.0" });

            var results = Flatten(CreateController("1.0.0").InvokeCommand().Cast<object>());

            Assert.Empty(results);
            _logger.Received().LogError(Arg.Any<Exception>());
        }

        [Fact]
        public void Guid_Version_Calls_GetPackageVersion()
        {
            var versionId = Guid.NewGuid();
            var expected = new PackageVersion { Id = versionId, Version = "3.0.0" };

            _client.GetPackageVersionAsync(
                    Arg.Any<string>(), FeedId, _package.Id.ToString(), versionId.ToString(),
                    Arg.Any<bool?>(), Arg.Any<bool?>(), Arg.Any<bool?>(),
                    Arg.Any<object>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(expected));

            var results = Flatten(CreateController(versionId.ToString()).InvokeCommand().Cast<object>());

            Assert.Single(results);
            Assert.Same(expected, results[0]);
        }

        [Fact]
        public void Guid_Version_Does_Not_Return_Deleted_Version_Without_IncludeDeleted()
        {
            var versionId = Guid.NewGuid();
            var deleted = new PackageVersion { Id = versionId, Version = "3.0.0", IsDeleted = true };

            _client.GetPackageVersionAsync(
                    Arg.Any<string>(), FeedId, _package.Id.ToString(), versionId.ToString(),
                    Arg.Any<bool?>(), Arg.Any<bool?>(), Arg.Any<bool?>(),
                    Arg.Any<object>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(deleted));

            var results = Flatten(CreateController(versionId.ToString()).InvokeCommand().Cast<object>());

            Assert.Empty(results);
        }

        [Fact]
        public void Guid_Version_Returns_Deleted_Version_With_IncludeDeleted()
        {
            var versionId = Guid.NewGuid();
            var deleted = new PackageVersion { Id = versionId, Version = "3.0.0", IsDeleted = true };

            _client.GetPackageVersionAsync(
                    Arg.Any<string>(), FeedId, _package.Id.ToString(), versionId.ToString(),
                    Arg.Any<bool?>(), Arg.Any<bool?>(), Arg.Any<bool?>(),
                    Arg.Any<object>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(deleted));

            var results = Flatten(CreateController(versionId.ToString(), includeDeleted: true)
                .InvokeCommand().Cast<object>());

            Assert.Single(results);
            Assert.Same(deleted, results[0]);
        }
    }
}
