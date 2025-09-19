namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers;

public partial class ControllerGeneratorTests
{
    [Fact]
    public async Task Can_Create_NewAreaController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(Can_Create_NewAreaController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\NewArea.cs"
            });
    }

}