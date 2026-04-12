namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Git.Item;

public partial class GitItemCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetGitItemCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetGitItemCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\Item\\GetGitItem.cs"
            });
    }
}