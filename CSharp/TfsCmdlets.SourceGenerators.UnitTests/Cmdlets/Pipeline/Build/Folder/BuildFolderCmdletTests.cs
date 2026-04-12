namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Pipeline.Build.Folder;

public partial class BuildFolderCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetBuildDefinitionFolderCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetBuildDefinitionFolderCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Folder\\GetBuildDefinitionFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewBuildDefinitionFolderCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewBuildDefinitionFolderCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Folder\\NewBuildDefinitionFolder.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveBuildDefinitionFolderCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveBuildDefinitionFolderCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Folder\\RemoveBuildDefinitionFolder.cs"
            });
    }
}