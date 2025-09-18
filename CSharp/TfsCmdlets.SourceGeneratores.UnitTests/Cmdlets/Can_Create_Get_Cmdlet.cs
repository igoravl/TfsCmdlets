namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class CmdletGeneratorTests
{
    [Fact]
    public async Task Can_Create_Get_Cmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(Can_Create_Get_Cmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\GetGitRepository.cs"
            });
    }

}