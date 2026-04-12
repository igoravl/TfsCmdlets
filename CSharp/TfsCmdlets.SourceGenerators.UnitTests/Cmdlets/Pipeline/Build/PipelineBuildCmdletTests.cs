namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Pipeline.Build;

public partial class PipelineBuildCmdletTests
{
    [Fact]
    public async Task CanGenerate_StartBuildCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_StartBuildCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Pipeline\\Build\\StartBuild.cs"
            });
    }
}