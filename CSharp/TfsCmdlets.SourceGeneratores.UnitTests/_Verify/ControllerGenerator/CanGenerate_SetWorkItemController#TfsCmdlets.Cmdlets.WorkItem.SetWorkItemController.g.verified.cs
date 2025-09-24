//HintName: TfsCmdlets.Cmdlets.WorkItem.SetWorkItemController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Extensions;
namespace TfsCmdlets.Cmdlets.WorkItem
{
    internal partial class SetWorkItemController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // WorkItem
        protected bool Has_WorkItem { get; set; }
        protected object WorkItem { get; set; }
        // Title
        protected bool Has_Title { get; set; }
        protected string Title { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
        // AreaPath
        protected bool Has_AreaPath { get; set; }
        protected string AreaPath { get; set; }
        // IterationPath
        protected bool Has_IterationPath { get; set; }
        protected string IterationPath { get; set; }
        // AssignedTo
        protected bool Has_AssignedTo { get; set; }
        protected object AssignedTo { get; set; }
        // State
        protected bool Has_State { get; set; }
        protected string State { get; set; }
        // Reason
        protected bool Has_Reason { get; set; }
        protected string Reason { get; set; }
        // ValueArea
        protected bool Has_ValueArea { get; set; }
        protected string ValueArea { get; set; }
        // BoardColumn
        protected bool Has_BoardColumn { get; set; }
        protected string BoardColumn { get; set; }
        // BoardColumnDone
        protected bool Has_BoardColumnDone { get; set; }
        protected bool BoardColumnDone { get; set; }
        // BoardLane
        protected bool Has_BoardLane { get; set; }
        protected string BoardLane { get; set; }
        // Priority
        protected bool Has_Priority { get; set; }
        protected int Priority { get; set; }
        // Tags
        protected bool Has_Tags { get; set; }
        protected string[] Tags { get; set; }
        // Fields
        protected bool Has_Fields { get; set; }
        protected System.Collections.Hashtable Fields { get; set; }
        // BypassRules
        protected bool Has_BypassRules { get; set; }
        protected bool BypassRules { get; set; }
        // SuppressNotifications
        protected bool Has_SuppressNotifications { get; set; }
        protected bool SuppressNotifications { get; set; }
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
        protected IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem> Items => WorkItem switch {
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem);
        protected override void CacheParameters()
        {
            // WorkItem
            Has_WorkItem = Parameters.HasParameter("WorkItem");
            WorkItem = Parameters.Get<object>("WorkItem");
            // Title
            Has_Title = Parameters.HasParameter("Title");
            Title = Parameters.Get<string>("Title");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // AreaPath
            Has_AreaPath = Parameters.HasParameter("AreaPath");
            AreaPath = Parameters.Get<string>("AreaPath");
            // IterationPath
            Has_IterationPath = Parameters.HasParameter("IterationPath");
            IterationPath = Parameters.Get<string>("IterationPath");
            // AssignedTo
            Has_AssignedTo = Parameters.HasParameter("AssignedTo");
            AssignedTo = Parameters.Get<object>("AssignedTo");
            // State
            Has_State = Parameters.HasParameter("State");
            State = Parameters.Get<string>("State");
            // Reason
            Has_Reason = Parameters.HasParameter("Reason");
            Reason = Parameters.Get<string>("Reason");
            // ValueArea
            Has_ValueArea = Parameters.HasParameter("ValueArea");
            ValueArea = Parameters.Get<string>("ValueArea");
            // BoardColumn
            Has_BoardColumn = Parameters.HasParameter("BoardColumn");
            BoardColumn = Parameters.Get<string>("BoardColumn");
            // BoardColumnDone
            Has_BoardColumnDone = Parameters.HasParameter("BoardColumnDone");
            BoardColumnDone = Parameters.Get<bool>("BoardColumnDone");
            // BoardLane
            Has_BoardLane = Parameters.HasParameter("BoardLane");
            BoardLane = Parameters.Get<string>("BoardLane");
            // Priority
            Has_Priority = Parameters.HasParameter("Priority");
            Priority = Parameters.Get<int>("Priority");
            // Tags
            Has_Tags = Parameters.HasParameter("Tags");
            Tags = Parameters.Get<string[]>("Tags");
            // Fields
            Has_Fields = Parameters.HasParameter("Fields");
            Fields = Parameters.Get<System.Collections.Hashtable>("Fields");
            // BypassRules
            Has_BypassRules = Parameters.HasParameter("BypassRules");
            BypassRules = Parameters.Get<bool>("BypassRules");
            // SuppressNotifications
            Has_SuppressNotifications = Parameters.HasParameter("SuppressNotifications");
            SuppressNotifications = Parameters.Get<bool>("SuppressNotifications");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public SetWorkItemController(IWorkItemPatchBuilder builder, TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Builder = builder;
            Client = client;
        }
    }
}