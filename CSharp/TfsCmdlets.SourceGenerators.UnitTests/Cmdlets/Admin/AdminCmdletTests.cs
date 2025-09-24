namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Admin;

public partial class AdminCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetInstallationPathCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetInstallationPathCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\GetInstallationPath.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetVersionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetVersionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\GetVersion.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetConfigurationServerConnectionStringCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetConfigurationServerConnectionStringCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Admin\\GetConfigurationServerConnectionString.cs"
            });
    }
}