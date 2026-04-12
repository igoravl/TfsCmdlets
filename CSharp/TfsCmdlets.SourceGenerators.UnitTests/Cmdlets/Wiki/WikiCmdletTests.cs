namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Wiki;

public partial class WikiCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetWikiCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetWikiCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Wiki\\GetWiki.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWikiCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewWikiCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Wiki\\NewWiki.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveWikiCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveWikiCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Wiki\\RemoveWiki.cs"
            });
    }
}