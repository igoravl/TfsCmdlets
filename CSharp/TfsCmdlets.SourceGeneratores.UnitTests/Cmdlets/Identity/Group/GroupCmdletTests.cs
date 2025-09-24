namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.Identity.Group;

public partial class GroupCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetGroupCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetGroupCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\GetGroup.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewGroupCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewGroupCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\NewGroup.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveGroupCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveGroupCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\RemoveGroup.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_AddGroupMemberCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_AddGroupMemberCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\AddGroupMember.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetGroupMemberCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetGroupMemberCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\GetGroupMember.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveGroupMemberCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveGroupMemberCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\RemoveGroupMember.cs"
            });
    }
}