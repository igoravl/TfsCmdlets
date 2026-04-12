namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Organization;

public partial class OrganizationCmdletTests
{
    [Fact]
    public async Task CanGenerate_ConnectOrganizationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ConnectOrganizationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Organization\\ConnectOrganization.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisconnectOrganizationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_DisconnectOrganizationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Organization\\DisconnectOrganization.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetOrganizationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetOrganizationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Organization\\GetOrganization.cs"
            });
    }
}