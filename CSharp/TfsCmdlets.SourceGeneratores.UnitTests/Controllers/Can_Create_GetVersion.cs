namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class ControllerGeneratorTests
{
    [Fact]
    public async Task Can_Create_GetVersion()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(Can_Create_GetVersion),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\GetVersion.cs"
            });
    }

}