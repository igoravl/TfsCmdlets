namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.TeamProject;

public partial class TeamProjectCmdletTests
{
    [Fact]
    public async Task CanGenerate_ConnectTeamProjectCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ConnectTeamProjectCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\ConnectTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisconnectTeamProjectCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_DisconnectTeamProjectCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\DisconnectTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamProjectCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamProjectCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\GetTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewTeamProjectCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewTeamProjectCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\NewTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamProjectCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveTeamProjectCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\RemoveTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameTeamProjectCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RenameTeamProjectCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\RenameTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetTeamProjectCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_SetTeamProjectCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\SetTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UndoTeamProjectRemovalCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_UndoTeamProjectRemovalCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\UndoTeamProjectRemoval.cs"
            });
    }
}