//HintName: TfsCmdlets.Cmdlets.Identity.Group.NewGroupController.g.cs
using Microsoft.VisualStudio.Services.Graph.Client;
using TfsCmdlets.Cmdlets.Identity.Group;
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    internal partial class NewGroupController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGraphHttpClient Client { get; }
        // Group
        protected bool Has_Group { get; set; }
        protected string Group { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
        // Scope
        protected bool Has_Scope { get; set; }
        protected TfsCmdlets.GroupScope Scope { get; set; }
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
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Graph.Client.GraphGroup);
        protected override void CacheParameters()
        {
            // Group
            Has_Group = Parameters.HasParameter("Group");
            Group = Parameters.Get<string>("Group");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // Scope
            Has_Scope = Parameters.HasParameter("Scope");
            Scope = Parameters.Get<TfsCmdlets.GroupScope>("Scope", GroupScope.Collection);
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewGroupController(IGraphHttpClient graphClient, TfsCmdlets.HttpClients.IGraphHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            GraphClient = graphClient;
            Client = client;
        }
    }
}