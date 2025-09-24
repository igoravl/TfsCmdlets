namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Shell;

public partial class ShellControllerTests
{
    [Fact]
    public async Task CanGenerate_EnterShellController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_EnterShellController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Shell\\EnterShell.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ExitShellController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ExitShellController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Shell\\ExitShell.cs"
            });
    }
}