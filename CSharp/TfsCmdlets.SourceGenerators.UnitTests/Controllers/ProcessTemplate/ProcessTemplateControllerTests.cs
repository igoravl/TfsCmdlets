namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.ProcessTemplate;

public partial class ProcessTemplateControllerTests
{
    [Fact]
    public async Task CanGenerate_ExportProcessTemplateController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ExportProcessTemplateController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\ExportProcessTemplate.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetProcessTemplateController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetProcessTemplateController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\GetProcessTemplate.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ImportProcessTemplateController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ImportProcessTemplateController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\ProcessTemplate\\ImportProcessTemplate.cs"
            });
    }
}