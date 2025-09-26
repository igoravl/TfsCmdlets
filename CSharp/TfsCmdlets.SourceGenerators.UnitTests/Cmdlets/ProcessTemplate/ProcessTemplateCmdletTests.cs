namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.ProcessTemplate;

public partial class ProcessTemplateCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetProcessTemplateCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetProcessTemplateCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\GetProcessTemplate.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ExportProcessTemplateCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ExportProcessTemplateCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\ExportProcessTemplate.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ImportProcessTemplateCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ImportProcessTemplateCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\ImportProcessTemplate.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewProcessTemplateCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewProcessTemplateCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\NewProcessTemplate.cs"
            });
    }
}