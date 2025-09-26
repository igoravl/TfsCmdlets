namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Pipeline;

public partial class PipelineControllerTests
{
    [Fact]
    public async Task CanGenerate_StartBuildController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_StartBuildController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\StartBuild.cs"
            });
    }
}