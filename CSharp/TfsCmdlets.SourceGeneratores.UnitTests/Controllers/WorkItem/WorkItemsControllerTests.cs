namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.WorkItem;

public partial class WorkItemsControllerTests
{
    [Fact]
    public async Task CanGenerate_GetWorkItemController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetWorkItemController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\GetWorkItem.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWorkItemController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewWorkItemController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\NewWorkItem.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveWorkItemController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveWorkItemController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\RemoveWorkItem.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetWorkItemController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_SetWorkItemController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\SetWorkItem.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_CopyWorkItemController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_CopyWorkItemController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\CopyWorkItem.cs"
            });
    }
}