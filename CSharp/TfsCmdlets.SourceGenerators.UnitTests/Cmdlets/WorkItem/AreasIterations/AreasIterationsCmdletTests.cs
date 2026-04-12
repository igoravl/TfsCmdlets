namespace TfsCmdlets.SourceGenerators.UnitTests.Cmdlets.WorkItem.AreasIterations;

public partial class AreasIterationsCmdletTests
{
    [Fact]
    public async Task CanGenerate_GetAreaCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetAreaCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\GetArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewAreaCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewAreaCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\NewArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveAreaCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveAreaCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\RemoveArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameAreaCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RenameAreaCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\RenameArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_MoveAreaCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_MoveAreaCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\MoveArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_CopyAreaCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_CopyAreaCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\CopyArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_TestAreaCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_TestAreaCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\TestArea.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_GetIterationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_GetIterationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\GetIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_NewIterationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_NewIterationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\NewIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RemoveIterationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RemoveIterationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\RemoveIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_RenameIterationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_RenameIterationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\RenameIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_MoveIterationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_MoveIterationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\MoveIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_CopyIterationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_CopyIterationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\CopyIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_TestIterationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_TestIterationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\TestIteration.cs"
            });
    }

    [Fact]
    public async Task CanGenerate_SetIterationCmdlet()
    {
        await TestHelper.VerifyFiles<Generators.Cmdlets.CmdletGenerator>(
            nameof(CanGenerate_SetIterationCmdlet),
            new[]
            {
                "TfsCmdlets\\Cmdlets\\WorkItem\\AreasIterations\\SetIteration.cs"
            });
    }
}