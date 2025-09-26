namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.TeamProject.Member;

public partial class MemberControllerTests
{
    [Fact]
    public async Task CanGenerate_GetTeamProjectMemberController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamProjectMemberController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProject\\Member\\GetTeamProjectMember.cs"
            });
    }
}