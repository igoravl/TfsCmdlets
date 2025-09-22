namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.WorkItem.Tagging;

public partial class TaggingControllerTests
{
    [Fact]
    public async Task CanGenerate_DisableWorkItemTagController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_DisableWorkItemTagController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\DisableWorkItemTag.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_EnableWorkItemTagController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_EnableWorkItemTagController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\EnableWorkItemTag.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveWorkItemTagController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveWorkItemTagController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\RemoveWorkItemTag.cs"
            });
    }
}