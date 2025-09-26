namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.TestManagement;

public partial class TestManagementControllerTests
{
    [Fact]
    public async Task CanGenerate_GetTestPlanController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetTestPlanController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\GetTestPlan.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewTestPlanController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewTestPlanController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\NewTestPlan.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveTestPlanController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveTestPlanController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\RemoveTestPlan.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_CopyTestPlanController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_CopyTestPlanController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\TestManagement\\CopyTestPlan.cs"
            });
    }
}