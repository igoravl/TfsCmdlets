namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Git.Commit;

public partial class GitCommitCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetGitCommitCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetGitCommitCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Commit\\GetGitCommit.cs"
            });
    }
}