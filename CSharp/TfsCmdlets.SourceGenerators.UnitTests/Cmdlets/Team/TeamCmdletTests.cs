namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Team;

public partial class TeamCmdletTests
{
    [Fact]
    public async Task CanGenerate_ConnectTeamCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ConnectTeamCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\ConnectTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisconnectTeamCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_DisconnectTeamCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\DisconnectTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\GetTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewTeamCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewTeamCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\NewTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveTeamCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\RemoveTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameTeamCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RenameTeamCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\RenameTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetTeamCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_SetTeamCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\SetTeam.cs"
            });
    }
}