namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.ProcessTemplate.Field;

public partial class FieldControllerTests
{
    [Fact]
    public async Task CanGenerate_GetProcessFieldDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetProcessFieldDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\Field\\GetProcessFieldDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewProcessFieldDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewProcessFieldDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\Field\\NewProcessFieldDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveProcessFieldDefinitionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveProcessFieldDefinitionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\Field\\RemoveProcessFieldDefinition.cs"
            });
    }
}