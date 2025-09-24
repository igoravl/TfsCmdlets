namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Team;

public partial class TeamControllerTests
{
    [Fact]
    public async Task CanGenerate_ConnectTeamController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ConnectTeamController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\ConnectTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisconnectTeamController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_DisconnectTeamController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\DisconnectTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\GetTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewTeamController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewTeamController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\NewTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveTeamController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\RemoveTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameTeamController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RenameTeamController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\RenameTeam.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetTeamController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_SetTeamController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\SetTeam.cs"
            });
    }
}