namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Git;

public partial class GitControllerTests
{
    [Fact]
    public async Task CanGenerate_GetGitRepositoryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetGitRepositoryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\GetGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewGitRepositoryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewGitRepositoryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\NewGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveGitRepositoryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveGitRepositoryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\RemoveGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameGitRepositoryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RenameGitRepositoryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\RenameGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_EnableGitRepositoryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_EnableGitRepositoryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\EnableGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisableGitRepositoryController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_DisableGitRepositoryController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\DisableGitRepository.cs"
            });
    }
}