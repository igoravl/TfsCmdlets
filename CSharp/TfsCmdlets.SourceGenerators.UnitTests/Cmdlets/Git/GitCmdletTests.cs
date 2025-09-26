namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Git;

public partial class GitCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetGitRepositoryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetGitRepositoryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\GetGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewGitRepositoryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewGitRepositoryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\NewGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveGitRepositoryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveGitRepositoryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\RemoveGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameGitRepositoryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RenameGitRepositoryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\RenameGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_EnableGitRepositoryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_EnableGitRepositoryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\EnableGitRepository.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_DisableGitRepositoryCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_DisableGitRepositoryCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Git\\DisableGitRepository.cs"
            });
    }
}