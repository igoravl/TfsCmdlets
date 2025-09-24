namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Team.TeamMember;

public partial class TeamMemberControllerTests
{
    [Fact]
    public async Task CanGenerate_AddTeamMemberController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_AddTeamMemberController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamMember\\AddTeamMember.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamMemberController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamMemberController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamMember\\GetTeamMember.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamMemberController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveTeamMemberController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamMember\\RemoveTeamMember.cs"
            });
    }
}