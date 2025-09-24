namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.TeamProjectCollection;

public partial class TeamProjectCollectionCmdletTests
{
    [Fact]
    public async Task CanGenerate_ConnectTeamProjectCollectionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_ConnectTeamProjectCollectionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\ConnectTeamProjectCollection.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisconnectTeamProjectCollectionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_DisconnectTeamProjectCollectionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\DisconnectTeamProjectCollection.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetTeamProjectCollectionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTeamProjectCollectionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\GetTeamProjectCollection.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewTeamProjectCollectionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewTeamProjectCollectionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\NewTeamProjectCollection.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTeamProjectCollectionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveTeamProjectCollectionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TeamProjectCollection\\RemoveTeamProjectCollection.cs"
            });
    }
}