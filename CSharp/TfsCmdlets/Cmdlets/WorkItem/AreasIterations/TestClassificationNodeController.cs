namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal abstract class TestClassificationNodeController: ControllerBase
    {
        protected override IEnumerable Run()
        {
            yield return Data.TestItem<Models.ClassificationNode>();
        }

        [ImportingConstructor]
        protected TestClassificationNodeController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}