namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Pipeline.Build.Folder;

public partial class FolderControllerTests
{
    [Fact]
    public async Task CanGenerate_GetBuildDefinitionFolderController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetBuildDefinitionFolderController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Folder\\GetBuildDefinitionFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewBuildDefinitionFolderController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewBuildDefinitionFolderController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Folder\\NewBuildDefinitionFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveBuildDefinitionFolderController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveBuildDefinitionFolderController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Folder\\RemoveBuildDefinitionFolder.cs"
            });
    }
}