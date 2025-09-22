namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Identity.User;

public partial class UserControllerTests
{
    [Fact]
    public async Task CanGenerate_GetUserController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetUserController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\User\\GetUser.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewUserController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewUserController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\User\\NewUser.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveUserController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveUserController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\User\\RemoveUser.cs"
            });
    }
}