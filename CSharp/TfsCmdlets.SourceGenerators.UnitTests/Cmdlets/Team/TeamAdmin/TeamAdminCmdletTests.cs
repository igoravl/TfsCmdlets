namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Team.TeamAdmin;

public partial class TeamAdminCmdletTests
{
    [Fact]
    public async Task CanGenerate_AddTeamAdminCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_AddTeamAdminCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamAdmin\\AddTeamAdmin.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamAdminCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamAdminCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamAdmin\\GetTeamAdmin.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamAdminCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveTeamAdminCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamAdmin\\RemoveTeamAdmin.cs"
            });
    }
}