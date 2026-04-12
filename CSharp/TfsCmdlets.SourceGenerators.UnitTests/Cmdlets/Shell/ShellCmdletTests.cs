namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Shell;

public partial class ShellCmdletTests
{
    [Fact]
    public async Task CanGenerate_EnterShellCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_EnterShellCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Shell\\EnterShell.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ExitShellCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ExitShellCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Shell\\ExitShell.cs"
            });
    }
}