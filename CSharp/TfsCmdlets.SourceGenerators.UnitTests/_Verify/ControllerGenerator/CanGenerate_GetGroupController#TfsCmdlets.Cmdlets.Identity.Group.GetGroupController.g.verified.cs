//HintName: TfsCmdlets.Cmdlets.Identity.Group.GetGroupController.g.cs
using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    internal partial class GetGroupController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGraphHttpClient Client { get; }
        // Group
        protected bool Has_Group => Parameters.HasParameter(nameof(Group));
        protected IEnumerable Group
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Group), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Scope
        protected bool Has_Scope { get; set; }
        protected TfsCmdlets.GroupScope Scope { get; set; }
        // Recurse
        protected bool Has_Recurse { get; set; }
        protected bool Recurse { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Graph.Client.GraphGroup);
        protected override void CacheParameters()
        {
            // Scope
            Has_Scope = Parameters.HasParameter("Scope");
            Scope = Parameters.Get<TfsCmdlets.GroupScope>("Scope", GroupScope.Collection);
            // Recurse
            Has_Recurse = Parameters.HasParameter("Recurse");
            Recurse = Parameters.Get<bool>("Recurse");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetGroupController(IGraphHttpClient graphClient, TfsCmdlets.HttpClients.IGraphHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            GraphClient = graphClient;
            Client = client;
        }
    }
}