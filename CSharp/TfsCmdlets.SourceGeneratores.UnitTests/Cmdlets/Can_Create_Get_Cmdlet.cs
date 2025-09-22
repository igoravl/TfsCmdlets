namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class CmdletGeneratorTests
{
    [Fact]
    public async Task CanGenerate_Get_Cmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_Get_Cmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\GetGitRepository.cs"
            });
    }

}