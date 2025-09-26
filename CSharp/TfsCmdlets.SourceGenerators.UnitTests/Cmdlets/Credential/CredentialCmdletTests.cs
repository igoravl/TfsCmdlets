namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Credential;

public partial class CredentialCmdletTests
{
    [Fact]
    public async Task CanGenerate_NewCredentialCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewCredentialCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Credential\\NewCredential.cs"
            });
    }
}