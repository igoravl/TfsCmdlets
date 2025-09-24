namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Wiki;

public partial class WikiControllerTests
{
    [Fact]
    public async Task CanGenerate_GetWikiController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetWikiController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Wiki\\GetWiki.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewWikiController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewWikiController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Wiki\\NewWiki.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveWikiController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveWikiController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Wiki\\RemoveWiki.cs"
            });
    }
}