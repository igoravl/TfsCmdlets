namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Git.Policy;

public partial class GitPolicyCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetGitPolicyTypeCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetGitPolicyTypeCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Policy\\GetGitPolicyType.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetGitBranchPolicyCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetGitBranchPolicyCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Policy\\GetGitBranchPolicy.cs"
            });
    }
}