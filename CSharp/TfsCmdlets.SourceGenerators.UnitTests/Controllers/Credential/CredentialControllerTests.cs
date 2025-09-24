namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Credential;

public partial class CredentialControllerTests
{
    [Fact]
    public async Task CanGenerate_NewCredentialController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewCredentialController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Credential\\NewCredential.cs"
            });
    }
}