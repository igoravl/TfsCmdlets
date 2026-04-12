namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.TestManagement;

public partial class TestManagementCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetTestPlanCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetTestPlanCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\GetTestPlan.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewTestPlanCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewTestPlanCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\NewTestPlan.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTestPlanCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveTestPlanCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\RemoveTestPlan.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameTestPlanCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RenameTestPlanCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\RenameTestPlan.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_CopyTestPlanCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_CopyTestPlanCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\CopyTestPlan.cs"
            });
    }
}