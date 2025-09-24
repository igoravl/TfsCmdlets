namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.WorkItem.Tagging;

public partial class WorkItemTaggingCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetWorkItemTagCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWorkItemTagCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\GetWorkItemTag.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWorkItemTagCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewWorkItemTagCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\NewWorkItemTag.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveWorkItemTagCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveWorkItemTagCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\RemoveWorkItemTag.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameWorkItemTagCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RenameWorkItemTagCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\RenameWorkItemTag.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_EnableWorkItemTagCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_EnableWorkItemTagCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\EnableWorkItemTag.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisableWorkItemTagCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_DisableWorkItemTagCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Tagging\\DisableWorkItemTag.cs"
            });
    }
}