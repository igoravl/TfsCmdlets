namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Team.Backlog;

public partial class BacklogControllerTests
{
    [Fact]
    public async Task CanGenerate_GetTeamBacklogLevelController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamBacklogLevelController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\Backlog\\GetTeamBacklogLevel.cs"
            });
    }
}