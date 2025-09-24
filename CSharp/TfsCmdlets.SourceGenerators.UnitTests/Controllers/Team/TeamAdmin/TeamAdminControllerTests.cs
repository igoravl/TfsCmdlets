namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Team.TeamAdmin;

public partial class TeamAdminControllerTests
{
    [Fact]
    public async Task CanGenerate_AddTeamAdminController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_AddTeamAdminController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamAdmin\\AddTeamAdmin.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamAdminController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamAdminController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamAdmin\\GetTeamAdmin.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamAdminController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveTeamAdminController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\TeamAdmin\\RemoveTeamAdmin.cs"
            });
    }
}