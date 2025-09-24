namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Identity;

public partial class IdentityControllerTests
{
    [Fact]
    public async Task CanGenerate_GetIdentityController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetIdentityController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\GetIdentity.cs"
            });
    }
}