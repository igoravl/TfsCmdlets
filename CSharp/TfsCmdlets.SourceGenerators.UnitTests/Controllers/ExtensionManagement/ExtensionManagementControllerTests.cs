namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.ExtensionManagement;

public partial class ExtensionManagementControllerTests
{
    [Fact]
    public async Task CanGenerate_GetExtensionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetExtensionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\GetExtension.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_InstallExtensionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_InstallExtensionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\InstallExtension.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UninstallExtensionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_UninstallExtensionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\UninstallExtension.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_EnableExtensionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_EnableExtensionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\EnableExtension.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisableExtensionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_DisableExtensionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ExtensionManagement\\DisableExtension.cs"
            });
    }
}