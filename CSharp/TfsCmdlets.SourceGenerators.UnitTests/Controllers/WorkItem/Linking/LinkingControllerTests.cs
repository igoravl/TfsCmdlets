namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.WorkItem.Linking;

public partial class LinkingControllerTests
{
    [Fact]
    public async Task CanGenerate_AddWorkItemLinkController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_AddWorkItemLinkController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Linking\\AddWorkItemLink.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ExportWorkItemAttachmentController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ExportWorkItemAttachmentController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Linking\\ExportWorkItemAttachment.cs"
            });
    }
}