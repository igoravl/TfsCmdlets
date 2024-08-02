namespace TfsCmdlets.Controllers
{
    public abstract class ControllerBase : IController
    {
        public string Verb => GetType().Name.Substring(0, GetType().Name.FindIndex(c => char.IsUpper(c), 1));

        public string Noun => GetType().Name.Substring(Verb.Length, GetType().Name.EndsWith("Controller") ? GetType().Name.Length - Verb.Length - 10 : GetType().Name.Length - Verb.Length);

        public string CmdletDisplayName => $"{Verb}-Tfs{Noun}";

        public string CommandName => $"{Verb}{Noun}";

        public virtual Type DataType => GetType();

        protected T GetItem<T>(object overridingParameters = null) => Data.GetItem<T>(overridingParameters);

        protected bool TestItem<T>(object overridingParameters = null) => Data.TestItem<T>(overridingParameters);

        protected IEnumerable<T> GetItems<T>(object overridingParameters = null) => Data.GetItems<T>(overridingParameters);

        protected IParameterManager Parameters { get; }

        protected ILogger Logger { get; }

        protected IDataManager Data { get; }

        protected IPowerShellService PowerShell { get; }

        protected abstract IEnumerable Run();

        protected abstract void CacheParameters();

        public IEnumerable InvokeCommand()
        {
            CacheParameters();

            Logger.LogParameters(CmdletDisplayName, Parameters);

            return Run();
        }

        [ImportingConstructor]
        public ControllerBase(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
        {
            PowerShell = powerShell;
            Data = data;
            Parameters = parameters;
            Logger = logger;
        }
    }
}