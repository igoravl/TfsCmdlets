//HintName: TfsCmdlets.Cmdlets.Identity.Group.RemoveGroupController.g.cs
using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    internal partial class RemoveGroupController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGraphHttpClient Client { get; }
        // Group
        protected bool Has_Group { get; set; }
        protected object Group { get; set; }
        // Scope
        protected bool Has_Scope { get; set; }
        protected TfsCmdlets.GroupScope Scope { get; set; }
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
        protected IEnumerable<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup> Items => Group switch {
            Microsoft.VisualStudio.Services.Graph.Client.GraphGroup item => new[] { item },
            IEnumerable<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup> items => items,
            _ => Data.GetItems<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Graph.Client.GraphGroup);
        protected override void CacheParameters()
        {
            // Group
            Has_Group = Parameters.HasParameter("Group");
            Group = Parameters.Get<object>("Group", "*");
            // Scope
            Has_Scope = Parameters.HasParameter("Scope");
            Scope = Parameters.Get<TfsCmdlets.GroupScope>("Scope", GroupScope.Collection);
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveGroupController(TfsCmdlets.HttpClients.IGraphHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}