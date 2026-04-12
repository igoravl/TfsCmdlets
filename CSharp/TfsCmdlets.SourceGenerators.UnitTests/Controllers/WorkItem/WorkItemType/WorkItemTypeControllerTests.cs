namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.WorkItem.WorkItemType;

public partial class WorkItemTypeControllerTests
{
    [Fact]
    public async Task CanGenerate_ExportWorkItemTypeController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ExportWorkItemTypeController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\WorkItemType\\ExportWorkItemType.cs"
            });
    }
}