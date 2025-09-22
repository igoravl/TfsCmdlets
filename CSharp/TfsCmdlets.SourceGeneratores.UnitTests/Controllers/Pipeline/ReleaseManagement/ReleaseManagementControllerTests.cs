namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Pipeline.ReleaseManagement;

public partial class ReleaseManagementControllerTests
{
    [Fact]
    public async Task CanGenerate_GetReleaseDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetReleaseDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\ReleaseManagement\\GetReleaseDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetReleaseDefinitionFolderController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetReleaseDefinitionFolderController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\ReleaseManagement\\GetReleaseDefinitionFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewReleaseDefinitionFolderController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewReleaseDefinitionFolderController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\ReleaseManagement\\NewReleaseDefinitionFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveReleaseDefinitionFolderController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveReleaseDefinitionFolderController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\ReleaseManagement\\RemoveReleaseDefinitionFolder.cs"
            });
    }
}