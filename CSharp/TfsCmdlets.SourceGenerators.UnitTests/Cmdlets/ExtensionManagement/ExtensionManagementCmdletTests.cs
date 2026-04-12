namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.ExtensionManagement;

public partial class ExtensionManagementCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetExtensionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetExtensionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\GetExtension.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_InstallExtensionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_InstallExtensionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\InstallExtension.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UninstallExtensionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_UninstallExtensionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\UninstallExtension.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_EnableExtensionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_EnableExtensionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\EnableExtension.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisableExtensionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_DisableExtensionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\DisableExtension.cs"
            });
    }
}