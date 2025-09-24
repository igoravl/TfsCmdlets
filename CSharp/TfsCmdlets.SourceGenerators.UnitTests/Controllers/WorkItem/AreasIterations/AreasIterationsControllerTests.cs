namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers.WorkItem.AreasIterations;

public partial class AreasIterationsControllerTests
{
    [Fact]
    public async Task CanGenerate_GetAreaController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetAreaController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\GetArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewAreaController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewAreaController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\NewArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveAreaController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveAreaController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\RemoveArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_CopyAreaController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_CopyAreaController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\CopyArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_MoveAreaController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_MoveAreaController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\MoveArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_TestAreaController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_TestAreaController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\TestArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetIterationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_GetIterationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\GetIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewIterationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_NewIterationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\NewIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveIterationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_RemoveIterationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\RemoveIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_CopyIterationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_CopyIterationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\CopyIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_MoveIterationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_MoveIterationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\MoveIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetIterationController()
    {
        await TestHelper.VerifyFiles<Generators.Controllers.ControllerGenerator>(
            nameof(CanGenerate_SetIterationController),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\SetIteration.cs"
            });
    }
}