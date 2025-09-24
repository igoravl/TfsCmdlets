namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.Identity.Group;

public partial class GroupControllerTests
{
    [Fact]
    public async Task CanGenerate_GetGroupController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetGroupController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\GetGroup.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewGroupController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewGroupController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\NewGroup.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveGroupController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveGroupController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\RemoveGroup.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetGroupMemberController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetGroupMemberController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\GetGroupMember.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_AddGroupMemberController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_AddGroupMemberController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\AddGroupMember.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveGroupMemberController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveGroupMemberController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\Identity\\Group\\RemoveGroupMember.cs"
            });
    }
}