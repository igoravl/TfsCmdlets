namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Admin.Registry;

public partial class RegistryControllerTests
{
    [Fact]
    public async Task CanGenerate_GetRegistryValueController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetRegistryValueController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\Registry\\GetRegistryValue.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetRegistryValueController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_SetRegistryValueController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\Registry\\SetRegistryValue.cs"
            });
    }
}