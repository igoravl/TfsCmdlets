namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.TeamProjectCollection;

public partial class TeamProjectCollectionControllerTests
{
    [Fact]
    public async Task CanGenerate_ConnectTeamProjectCollectionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_ConnectTeamProjectCollectionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\ConnectTeamProjectCollection.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisconnectTeamProjectCollectionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_DisconnectTeamProjectCollectionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\DisconnectTeamProjectCollection.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamProjectCollectionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTeamProjectCollectionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\GetTeamProjectCollection.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewTeamProjectCollectionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewTeamProjectCollectionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\NewTeamProjectCollection.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamProjectCollectionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveTeamProjectCollectionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\RemoveTeamProjectCollection.cs"
            });
    }
}