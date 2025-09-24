namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.WorkItem.WorkItemType;

public partial class WorkItemTypeCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetWorkItemTypeCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWorkItemTypeCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\WorkItemType\\GetWorkItemType.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ExportWorkItemTypeCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ExportWorkItemTypeCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\WorkItemType\\ExportWorkItemType.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ImportWorkItemTypeCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ImportWorkItemTypeCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\WorkItemType\\ImportWorkItemType.cs"
            });
    }
}