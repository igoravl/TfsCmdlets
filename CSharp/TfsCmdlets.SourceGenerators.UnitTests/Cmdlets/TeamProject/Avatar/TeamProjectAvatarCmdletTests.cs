namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.TeamProject.Avatar;

public partial class TeamProjectAvatarCmdletTests
{
    [Fact]
    public async Task CanGenerate_RemoveTeamProjectAvatarCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveTeamProjectAvatarCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\Avatar\\RemoveTeamProjectAvatar.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ExportTeamProjectAvatarCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ExportTeamProjectAvatarCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\Avatar\\ExportTeamProjectAvatar.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_ImportTeamProjectAvatarCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ImportTeamProjectAvatarCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\Avatar\\ImportTeamProjectAvatar.cs"
            });
    }
}