namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class ControllerGeneratorTests
{
    [Fact]
    public async Task Can_Create_GetConfigurationServerConnectionString()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(Can_Create_GetConfigurationServerConnectionString),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\GetConfigurationServerConnectionString.cs"
            });
    }

}