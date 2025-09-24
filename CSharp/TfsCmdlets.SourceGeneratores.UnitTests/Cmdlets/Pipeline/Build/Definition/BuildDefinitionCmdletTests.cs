namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Pipeline.Build.Definition;

public partial class BuildDefinitionCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetBuildDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetBuildDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\GetBuildDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisableBuildDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_DisableBuildDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\DisableBuildDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_EnableBuildDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_EnableBuildDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\EnableBuildDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ResumeBuildDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ResumeBuildDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\ResumeBuildDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SuspendBuildDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_SuspendBuildDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\SuspendBuildDefinition.cs"
            });
    }
}