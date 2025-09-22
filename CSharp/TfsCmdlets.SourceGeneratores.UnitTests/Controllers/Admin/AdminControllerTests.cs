namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Admin;

public partial class AdminControllerTests
{
    [Fact]
    public async Task CanGenerate_GetVersionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetVersionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\GetVersion.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetConfigurationServerConnectionStringController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetConfigurationServerConnectionStringController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\GetConfigurationServerConnectionString.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetInstallationPathController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetInstallationPathController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\GetInstallationPath.cs"
            });
    }
}