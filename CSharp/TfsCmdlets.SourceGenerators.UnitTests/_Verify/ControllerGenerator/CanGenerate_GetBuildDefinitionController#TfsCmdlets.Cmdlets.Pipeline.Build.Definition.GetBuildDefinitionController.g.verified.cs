//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Definition.GetBuildDefinitionController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Definition
{
    internal partial class GetBuildDefinitionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IBuildHttpClient Client { get; }
        // Definition
        protected bool Has_Definition => Parameters.HasParameter(nameof(Definition));
        protected IEnumerable Definition
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Definition), "\\**");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // QueryOrder
        protected bool Has_QueryOrder { get; set; }
        protected Microsoft.TeamFoundation.Build.WebApi.DefinitionQueryOrder QueryOrder { get; set; }
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
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference);
        protected override void CacheParameters()
        {
            // QueryOrder
            Has_QueryOrder = Parameters.HasParameter("QueryOrder");
            QueryOrder = Parameters.Get<Microsoft.TeamFoundation.Build.WebApi.DefinitionQueryOrder>("QueryOrder");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetBuildDefinitionController(INodeUtil nodeUtil, TfsCmdlets.HttpClients.IBuildHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            NodeUtil = nodeUtil;
            Client = client;
        }
    }
}