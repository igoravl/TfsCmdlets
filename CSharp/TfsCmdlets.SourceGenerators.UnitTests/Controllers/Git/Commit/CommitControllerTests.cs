namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Git.Commit;

public partial class CommitControllerTests
{
    [Fact]
    public async Task CanGenerate_GetGitCommitController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetGitCommitController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Commit\\GetGitCommit.cs"
            });
    }
}