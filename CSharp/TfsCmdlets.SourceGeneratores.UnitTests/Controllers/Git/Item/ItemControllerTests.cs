namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Git.Item;

public partial class ItemControllerTests
{
    [Fact]
    public async Task CanGenerate_GetGitItemController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetGitItemController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Item\\GetGitItem.cs"
            });
    }
}