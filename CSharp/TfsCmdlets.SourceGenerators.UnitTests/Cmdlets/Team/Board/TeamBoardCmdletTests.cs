namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Team.Board;

public partial class TeamBoardCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetTeamBoardCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamBoardCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\Board\\GetTeamBoard.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamBoardCardRuleCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamBoardCardRuleCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\Board\\GetTeamBoardCardRule.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetTeamBoardCardRuleCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_SetTeamBoardCardRuleCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\Board\\SetTeamBoardCardRule.cs"
            });
    }
}