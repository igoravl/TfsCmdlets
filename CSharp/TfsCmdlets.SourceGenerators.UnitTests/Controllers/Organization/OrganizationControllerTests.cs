namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Organization;

public partial class OrganizationControllerTests
{
    [Fact]
    public async Task CanGenerate_ConnectOrganizationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ConnectOrganizationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Organization\\ConnectOrganization.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisconnectOrganizationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_DisconnectOrganizationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Organization\\DisconnectOrganization.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetOrganizationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetOrganizationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Organization\\GetOrganization.cs"
            });
    }
}