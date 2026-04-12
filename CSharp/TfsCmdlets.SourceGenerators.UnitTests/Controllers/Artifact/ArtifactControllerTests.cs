namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Artifact;

public partial class ArtifactControllerTests
{
    [Fact]
    public async Task CanGenerate_GetArtifactController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetArtifactController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Artifact\\GetArtifact.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetArtifactFeedController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetArtifactFeedController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Artifact\\GetArtifactFeed.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetArtifactFeedViewController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetArtifactFeedViewController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Artifact\\GetArtifactFeedView.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetArtifactVersionController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetArtifactVersionController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Artifact\\GetArtifactVersion.cs"
            });
    }
}