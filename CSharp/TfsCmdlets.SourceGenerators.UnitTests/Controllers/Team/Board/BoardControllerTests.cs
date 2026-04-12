namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Team.Board;

public partial class BoardControllerTests
{
    [Fact]
    public async Task CanGenerate_GetTeamBoardController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamBoardController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\Board\\GetTeamBoard.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamBoardCardRuleController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamBoardCardRuleController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\Board\\GetTeamBoardCardRule.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetTeamBoardCardRuleController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_SetTeamBoardCardRuleController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Team\\Board\\SetTeamBoardCardRule.cs"
            });
    }
}