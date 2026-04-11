namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Identity.PersonalAccessToken;

public partial class PersonalAccessTokenCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetPersonalAccessTokenCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetPersonalAccessTokenCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\GetPersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewPersonalAccessTokenCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewPersonalAccessTokenCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\NewPersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetPersonalAccessTokenCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_SetPersonalAccessTokenCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\SetPersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemovePersonalAccessTokenCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemovePersonalAccessTokenCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\RemovePersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenamePersonalAccessTokenCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RenamePersonalAccessTokenCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\RenamePersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UpdatePersonalAccessTokenCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_UpdatePersonalAccessTokenCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\UpdatePersonalAccessToken.cs"
            });
    }
}
