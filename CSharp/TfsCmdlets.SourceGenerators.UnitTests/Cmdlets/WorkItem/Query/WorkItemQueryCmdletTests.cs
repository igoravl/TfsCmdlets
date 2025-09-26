namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.WorkItem.Query;

public partial class WorkItemQueryCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetWorkItemQueryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWorkItemQueryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\GetWorkItemQuery.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetWorkItemQueryItemControllerCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWorkItemQueryItemControllerCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\GetWorkItemQueryItemController.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWorkItemQueryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewWorkItemQueryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\NewWorkItemQuery.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWorkItemQueryItemControllerCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewWorkItemQueryItemControllerCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\NewWorkItemQueryItemController.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ExportWorkItemQueryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ExportWorkItemQueryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\ExportWorkItemQuery.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UndoWorkItemQueryRemovalCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_UndoWorkItemQueryRemovalCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\UndoWorkItemQueryRemoval.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetWorkItemQueryFolderCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWorkItemQueryFolderCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\GetWorkItemQueryFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWorkItemQueryFolderCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewWorkItemQueryFolderCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\NewWorkItemQueryFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UndoWorkItemQueryFolderRemovalCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_UndoWorkItemQueryFolderRemovalCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\UndoWorkItemQueryFolderRemoval.cs"
            });
    }
}