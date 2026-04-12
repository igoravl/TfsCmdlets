namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Identity.PersonalAccessToken;

public partial class PersonalAccessTokenControllerTests
{
    [Fact]
    public async Task CanGenerate_GetPersonalAccessTokenController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetPersonalAccessTokenController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\GetPersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewPersonalAccessTokenController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewPersonalAccessTokenController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\NewPersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetPersonalAccessTokenController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_SetPersonalAccessTokenController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\SetPersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemovePersonalAccessTokenController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemovePersonalAccessTokenController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\RemovePersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenamePersonalAccessTokenController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RenamePersonalAccessTokenController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\RenamePersonalAccessToken.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_UpdatePersonalAccessTokenController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_UpdatePersonalAccessTokenController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\PersonalAccessToken\\UpdatePersonalAccessToken.cs"
            });
    }
}
