namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class ControllerGeneratorTests
{
    [Fact]
    public async Task Can_Create_GetCredential()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(Can_Create_GetCredential),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Credential\\NewCredential.cs"
            });
    }

}