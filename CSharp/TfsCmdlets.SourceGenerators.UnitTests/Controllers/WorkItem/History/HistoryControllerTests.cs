namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.WorkItem.History;

public partial class HistoryControllerTests
{
    [Fact]
    public async Task CanGenerate_GetWorkItemHistoryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetWorkItemHistoryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\History\\GetWorkItemHistory.cs"
            });
    }
}