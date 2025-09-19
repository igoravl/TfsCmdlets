namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class ControllerGeneratorTests
{
    [Fact]
    public async Task Can_Create_DisableExtension()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(Can_Create_DisableExtension),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\DisableExtension.cs"
            });
    }

}