namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.ProcessTemplate.Field;

public partial class ProcessFieldCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetProcessFieldDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetProcessFieldDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\Field\\GetProcessFieldDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewProcessFieldDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewProcessFieldDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\Field\\NewProcessFieldDefinition.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveProcessFieldDefinitionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveProcessFieldDefinitionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\Field\\RemoveProcessFieldDefinition.cs"
            });
    }
}