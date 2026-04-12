namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.TeamProject.Member;

public partial class TeamProjectMemberCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetTeamProjectMemberCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamProjectMemberCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\Member\\GetTeamProjectMember.cs"
            });
    }
}