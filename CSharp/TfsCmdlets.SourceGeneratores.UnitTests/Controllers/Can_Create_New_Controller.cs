namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class ControllerGeneratorTests
{
    [Fact]
    public async Task Can_Create_New_Controller()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(Can_Create_New_Controller),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\NewGitRepository.cs"
            });
    }

}