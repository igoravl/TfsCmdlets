namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Git.Branch;

public partial class BranchControllerTests
{
    [Fact]
    public async Task CanGenerate_GetGitBranchController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetGitBranchController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Branch\\GetGitBranch.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveGitBranchController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveGitBranchController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Branch\\RemoveGitBranch.cs"
            });
    }
}