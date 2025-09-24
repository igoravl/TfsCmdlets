namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.TeamProject;

public partial class TeamProjectControllerTests
{
    [Fact]
    public async Task CanGenerate_ConnectTeamProjectController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ConnectTeamProjectController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\ConnectTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisconnectTeamProjectController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_DisconnectTeamProjectController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\DisconnectTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamProjectController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamProjectController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\GetTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewTeamProjectController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewTeamProjectController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\NewTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamProjectController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveTeamProjectController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\RemoveTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameTeamProjectController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RenameTeamProjectController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\RenameTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetTeamProjectController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_SetTeamProjectController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\SetTeamProject.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UndoTeamProjectRemovalController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_UndoTeamProjectRemovalController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\UndoTeamProjectRemoval.cs"
            });
    }
}