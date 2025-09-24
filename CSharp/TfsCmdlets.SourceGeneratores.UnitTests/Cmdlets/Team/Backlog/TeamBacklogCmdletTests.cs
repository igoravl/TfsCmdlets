namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Team.Backlog;

public partial class TeamBacklogCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetTeamBacklogLevelCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamBacklogLevelCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\Backlog\\GetTeamBacklogLevel.cs"
            });
    }
}