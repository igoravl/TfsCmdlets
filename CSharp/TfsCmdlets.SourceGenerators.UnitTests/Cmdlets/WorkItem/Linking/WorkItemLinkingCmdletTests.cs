namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.WorkItem.Linking;

public partial class WorkItemLinkingCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetWorkItemLinkCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWorkItemLinkCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Linking\\GetWorkItemLink.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_AddWorkItemLinkCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_AddWorkItemLinkCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Linking\\AddWorkItemLink.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetWorkItemLinkEndTypeCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWorkItemLinkEndTypeCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Linking\\GetWorkItemLinkEndType.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ExportWorkItemAttachmentCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ExportWorkItemAttachmentCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Linking\\ExportWorkItemAttachment.cs"
            });
    }
}