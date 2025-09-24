//HintName: TfsCmdlets.Cmdlets.WorkItem.Linking.AddWorkItemLinkController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    internal partial class AddWorkItemLinkController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // WorkItem
        protected bool Has_WorkItem { get; set; }
        protected object WorkItem { get; set; }
        // TargetWorkItem
        protected bool Has_TargetWorkItem { get; set; }
        protected object TargetWorkItem { get; set; }
        // LinkType
        protected bool Has_LinkType { get; set; }
        protected TfsCmdlets.WorkItemLinkType LinkType { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
        // BypassRules
        protected bool Has_BypassRules { get; set; }
        protected bool BypassRules { get; set; }
        // SuppressNotifications
        protected bool Has_SuppressNotifications { get; set; }
        protected bool SuppressNotifications { get; set; }
        // Comment
        protected bool Has_Comment { get; set; }
        protected string Comment { get; set; }
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
        protected IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation> Items => WorkItem switch {
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation);
        protected override void CacheParameters()
        {
            // WorkItem
            Has_WorkItem = Parameters.HasParameter("WorkItem");
            WorkItem = Parameters.Get<object>("WorkItem");
            // TargetWorkItem
            Has_TargetWorkItem = Parameters.HasParameter("TargetWorkItem");
            TargetWorkItem = Parameters.Get<object>("TargetWorkItem");
            // LinkType
            Has_LinkType = Parameters.HasParameter("LinkType");
            LinkType = Parameters.Get<TfsCmdlets.WorkItemLinkType>("LinkType");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // BypassRules
            Has_BypassRules = Parameters.HasParameter("BypassRules");
            BypassRules = Parameters.Get<bool>("BypassRules");
            // SuppressNotifications
            Has_SuppressNotifications = Parameters.HasParameter("SuppressNotifications");
            SuppressNotifications = Parameters.Get<bool>("SuppressNotifications");
            // Comment
            Has_Comment = Parameters.HasParameter("Comment");
            Comment = Parameters.Get<string>("Comment");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public AddWorkItemLinkController(IKnownWorkItemLinkTypes knownLinkTypes, TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            KnownLinkTypes = knownLinkTypes;
            Client = client;
        }
    }
}