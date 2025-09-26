namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.TeamProject.Avatar;

public partial class AvatarControllerTests
{
    [Fact]
    public async Task CanGenerate_ExportTeamProjectAvatarController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ExportTeamProjectAvatarController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\Avatar\\ExportTeamProjectAvatar.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ImportTeamProjectAvatarController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ImportTeamProjectAvatarController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\Avatar\\ImportTeamProjectAvatar.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamProjectAvatarController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveTeamProjectAvatarController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\Avatar\\RemoveTeamProjectAvatar.cs"
            });
    }
}