namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.WorkItem.Query;

public partial class QueryControllerTests
{
    [Fact]
    public async Task CanGenerate_ExportWorkItemQueryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ExportWorkItemQueryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\ExportWorkItemQuery.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetWorkItemQueryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetWorkItemQueryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\GetWorkItemQuery.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetWorkItemQueryFolderController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetWorkItemQueryFolderController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\GetWorkItemQueryFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWorkItemQueryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewWorkItemQueryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\NewWorkItemQuery.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWorkItemQueryFolderController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewWorkItemQueryFolderController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\NewWorkItemQueryFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UndoWorkItemQueryFolderRemovalController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_UndoWorkItemQueryFolderRemovalController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\UndoWorkItemQueryFolderRemoval.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UndoWorkItemQueryRemovalController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_UndoWorkItemQueryRemovalController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\Query\\UndoWorkItemQueryRemoval.cs"
            });
    }
}