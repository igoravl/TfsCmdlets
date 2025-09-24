namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.WorkItem.History;

public partial class WorkItemHistoryCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetWorkItemHistoryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWorkItemHistoryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\History\\GetWorkItemHistory.cs"
            });
    }
}