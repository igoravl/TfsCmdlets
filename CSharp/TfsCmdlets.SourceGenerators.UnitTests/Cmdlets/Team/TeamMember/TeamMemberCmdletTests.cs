namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Team.TeamMember;

public partial class TeamMemberCmdletTests
{
    [Fact]
    public async Task CanGenerate_AddTeamMemberCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_AddTeamMemberCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamMember\\AddTeamMember.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamMemberCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamMemberCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamMember\\GetTeamMember.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamMemberCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveTeamMemberCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamMember\\RemoveTeamMember.cs"
            });
    }
}