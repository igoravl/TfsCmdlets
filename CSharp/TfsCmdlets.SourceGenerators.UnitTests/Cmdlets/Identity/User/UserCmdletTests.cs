namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Identity.User;

public partial class UserCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetUserCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetUserCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\User\\GetUser.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewUserCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewUserCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\User\\NewUser.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveUserCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveUserCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\User\\RemoveUser.cs"
            });
    }
}