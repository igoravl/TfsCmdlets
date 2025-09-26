namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Pipeline.Build.Definition;

public partial class DefinitionControllerTests
{
    [Fact]
    public async Task CanGenerate_GetBuildDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetBuildDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\GetBuildDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisableBuildDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_DisableBuildDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\DisableBuildDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_EnableBuildDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_EnableBuildDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\EnableBuildDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ResumeBuildDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ResumeBuildDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\ResumeBuildDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SuspendBuildDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_SuspendBuildDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\Definition\\SuspendBuildDefinition.cs"
            });
    }
}