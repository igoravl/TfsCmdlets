namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Git.Policy;

public partial class PolicyControllerTests
{
    [Fact]
    public async Task CanGenerate_GetGitBranchPolicyController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetGitBranchPolicyController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Policy\\GetGitBranchPolicy.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetGitPolicyTypeController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetGitPolicyTypeController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Policy\\GetGitPolicyType.cs"
            });
    }
}