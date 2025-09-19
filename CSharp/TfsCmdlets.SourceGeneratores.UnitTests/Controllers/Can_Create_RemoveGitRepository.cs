namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class ControllerGeneratorTests
{
    [Fact]
    public async Task Can_Create_RemoveGitRepository()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(Can_Create_RemoveGitRepository),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\RemoveGitRepository.cs"
            });
    }

}