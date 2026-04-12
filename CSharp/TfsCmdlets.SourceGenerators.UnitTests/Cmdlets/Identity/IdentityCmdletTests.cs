namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Identity;

public partial class IdentityCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetIdentityCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetIdentityCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\GetIdentity.cs"
            });
    }
}