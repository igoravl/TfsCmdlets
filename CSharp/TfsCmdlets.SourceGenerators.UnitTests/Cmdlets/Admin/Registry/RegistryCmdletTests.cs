namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Admin.Registry;

public partial class RegistryCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetRegistryValueCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetRegistryValueCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\Registry\\GetRegistryValue.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetRegistryValueCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_SetRegistryValueCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\Registry\\SetRegistryValue.cs"
            });
    }
}