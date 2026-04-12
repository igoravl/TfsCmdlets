namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Pipeline.ReleaseManagement;

public partial class ReleaseManagementCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetReleaseDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetReleaseDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\ReleaseManagement\\GetReleaseDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetReleaseDefinitionFolderCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetReleaseDefinitionFolderCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\ReleaseManagement\\GetReleaseDefinitionFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewReleaseDefinitionFolderCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewReleaseDefinitionFolderCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\ReleaseManagement\\NewReleaseDefinitionFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveReleaseDefinitionFolderCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveReleaseDefinitionFolderCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\ReleaseManagement\\RemoveReleaseDefinitionFolder.cs"
            });
    }
}