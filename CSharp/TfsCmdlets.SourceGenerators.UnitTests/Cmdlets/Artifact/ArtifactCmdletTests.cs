namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Artifact;

public partial class ArtifactCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetArtifactCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetArtifactCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Artifact\\GetArtifact.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetArtifactFeedCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetArtifactFeedCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Artifact\\GetArtifactFeed.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetArtifactFeedViewCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetArtifactFeedViewCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Artifact\\GetArtifactFeedView.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetArtifactVersionCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetArtifactVersionCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Artifact\\GetArtifactVersion.cs"
            });
    }
}