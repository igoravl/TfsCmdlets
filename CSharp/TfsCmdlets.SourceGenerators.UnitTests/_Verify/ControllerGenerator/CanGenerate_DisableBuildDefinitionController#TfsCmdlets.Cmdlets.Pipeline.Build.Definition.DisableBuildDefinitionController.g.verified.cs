//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Definition.DisableBuildDefinitionController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Definition
{
    internal partial class DisableBuildDefinitionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IBuildHttpClient Client { get; }
        // Definition
        protected bool Has_Definition { get; set; }
        protected object Definition { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
        // Project
        protected bool Has_Project => Parameters.HasParameter("Project");
        protected WebApiTeamProject Project => Data.GetProject();
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // Items
        protected IEnumerable<Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference> Items => Definition switch {
            Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference);
        protected override void CacheParameters()
        {
            // Definition
            Has_Definition = Parameters.HasParameter("Definition");
            Definition = Parameters.Get<object>("Definition");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public DisableBuildDefinitionController(TfsCmdlets.HttpClients.IBuildHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}