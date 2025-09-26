namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Git.Branch;

public partial class GitBranchCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetGitBranchCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetGitBranchCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Branch\\GetGitBranch.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveGitBranchCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveGitBranchCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Branch\\RemoveGitBranch.cs"
            });
    }
}