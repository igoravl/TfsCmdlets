//HintName: TfsCmdlets.Cmdlets.WorkItem.GetWorkItemController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
namespace TfsCmdlets.Cmdlets.WorkItem
{
    internal partial class GetWorkItemController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // WorkItem
        protected bool Has_WorkItem => Parameters.HasParameter(nameof(WorkItem));
        protected IEnumerable WorkItem
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(WorkItem));
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Title
        protected bool Has_Title { get; set; }
        protected string[] Title { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string[] Description { get; set; }
        // AreaPath
        protected bool Has_AreaPath { get; set; }
        protected string AreaPath { get; set; }
        // IterationPath
        protected bool Has_IterationPath { get; set; }
        protected string IterationPath { get; set; }
        // WorkItemType
        protected bool Has_WorkItemType { get; set; }
        protected string[] WorkItemType { get; set; }
        // State
        protected bool Has_State { get; set; }
        protected string[] State { get; set; }
        // Reason
        protected bool Has_Reason { get; set; }
        protected string[] Reason { get; set; }
        // ValueArea
        protected bool Has_ValueArea { get; set; }
        protected string[] ValueArea { get; set; }
        // BoardColumn
        protected bool Has_BoardColumn { get; set; }
        protected string[] BoardColumn { get; set; }
        // BoardColumnDone
        protected bool Has_BoardColumnDone { get; set; }
        protected bool BoardColumnDone { get; set; }
        // CreatedBy
        protected bool Has_CreatedBy { get; set; }
        protected object[] CreatedBy { get; set; }
        // CreatedDate
        protected bool Has_CreatedDate { get; set; }
        protected System.DateTime[] CreatedDate { get; set; }
        // ChangedBy
        protected bool Has_ChangedBy { get; set; }
        protected object ChangedBy { get; set; }
        // ChangedDate
        protected bool Has_ChangedDate { get; set; }
        protected System.DateTime[] ChangedDate { get; set; }
        // StateChangeDate
        protected bool Has_StateChangeDate { get; set; }
        protected System.DateTime[] StateChangeDate { get; set; }
        // Priority
        protected bool Has_Priority { get; set; }
        protected int[] Priority { get; set; }
        // Tags
        protected bool Has_Tags { get; set; }
        protected string[] Tags { get; set; }
        // Ever
        protected bool Has_Ever { get; set; }
        protected bool Ever { get; set; }
        // Revision
        protected bool Has_Revision { get; set; }
        protected int Revision { get; set; }
        // AsOf
        protected bool Has_AsOf { get; set; }
        protected System.DateTime AsOf { get; set; }
        // Wiql
        protected bool Has_Wiql { get; set; }
        protected string Wiql { get; set; }
        // SavedQuery
        protected bool Has_SavedQuery { get; set; }
        protected string SavedQuery { get; set; }
        // QueryParameters
        protected bool Has_QueryParameters { get; set; }
        protected System.Collections.Hashtable QueryParameters { get; set; }
        // Fields
        protected bool Has_Fields { get; set; }
        protected string[] Fields { get; set; }
        // Where
        protected bool Has_Where { get; set; }
        protected string Where { get; set; }
        // TimePrecision
        protected bool Has_TimePrecision { get; set; }
        protected bool TimePrecision { get; set; }
        // ShowWindow
        protected bool Has_ShowWindow { get; set; }
        protected bool ShowWindow { get; set; }
        // Deleted
        protected bool Has_Deleted { get; set; }
        protected bool Deleted { get; set; }
        // IncludeLinks
        protected bool Has_IncludeLinks { get; set; }
        protected bool IncludeLinks { get; set; }
        // Team
        protected bool Has_Team => Parameters.HasParameter("Team");
        protected WebApiTeam Team => Data.GetTeam();
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem);
        protected override void CacheParameters()
        {
            // Title
            Has_Title = Parameters.HasParameter("Title");
            Title = Parameters.Get<string[]>("Title");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string[]>("Description");
            // AreaPath
            Has_AreaPath = Parameters.HasParameter("AreaPath");
            AreaPath = Parameters.Get<string>("AreaPath");
            // IterationPath
            Has_IterationPath = Parameters.HasParameter("IterationPath");
            IterationPath = Parameters.Get<string>("IterationPath");
            // WorkItemType
            Has_WorkItemType = Parameters.HasParameter("WorkItemType");
            WorkItemType = Parameters.Get<string[]>("WorkItemType");
            // State
            Has_State = Parameters.HasParameter("State");
            State = Parameters.Get<string[]>("State");
            // Reason
            Has_Reason = Parameters.HasParameter("Reason");
            Reason = Parameters.Get<string[]>("Reason");
            // ValueArea
            Has_ValueArea = Parameters.HasParameter("ValueArea");
            ValueArea = Parameters.Get<string[]>("ValueArea");
            // BoardColumn
            Has_BoardColumn = Parameters.HasParameter("BoardColumn");
            BoardColumn = Parameters.Get<string[]>("BoardColumn");
            // BoardColumnDone
            Has_BoardColumnDone = Parameters.HasParameter("BoardColumnDone");
            BoardColumnDone = Parameters.Get<bool>("BoardColumnDone");
            // CreatedBy
            Has_CreatedBy = Parameters.HasParameter("CreatedBy");
            CreatedBy = Parameters.Get<object[]>("CreatedBy");
            // CreatedDate
            Has_CreatedDate = Parameters.HasParameter("CreatedDate");
            CreatedDate = Parameters.Get<System.DateTime[]>("CreatedDate");
            // ChangedBy
            Has_ChangedBy = Parameters.HasParameter("ChangedBy");
            ChangedBy = Parameters.Get<object>("ChangedBy");
            // ChangedDate
            Has_ChangedDate = Parameters.HasParameter("ChangedDate");
            ChangedDate = Parameters.Get<System.DateTime[]>("ChangedDate");
            // StateChangeDate
            Has_StateChangeDate = Parameters.HasParameter("StateChangeDate");
            StateChangeDate = Parameters.Get<System.DateTime[]>("StateChangeDate");
            // Priority
            Has_Priority = Parameters.HasParameter("Priority");
            Priority = Parameters.Get<int[]>("Priority");
            // Tags
            Has_Tags = Parameters.HasParameter("Tags");
            Tags = Parameters.Get<string[]>("Tags");
            // Ever
            Has_Ever = Parameters.HasParameter("Ever");
            Ever = Parameters.Get<bool>("Ever");
            // Revision
            Has_Revision = Parameters.HasParameter("Revision");
            Revision = Parameters.Get<int>("Revision");
            // AsOf
            Has_AsOf = Parameters.HasParameter("AsOf");
            AsOf = Parameters.Get<System.DateTime>("AsOf");
            // Wiql
            Has_Wiql = Parameters.HasParameter("Wiql");
            Wiql = Parameters.Get<string>("Wiql");
            // SavedQuery
            Has_SavedQuery = Parameters.HasParameter("SavedQuery");
            SavedQuery = Parameters.Get<string>("SavedQuery");
            // QueryParameters
            Has_QueryParameters = Parameters.HasParameter("QueryParameters");
            QueryParameters = Parameters.Get<System.Collections.Hashtable>("QueryParameters");
            // Fields
            Has_Fields = Parameters.HasParameter("Fields");
            Fields = Parameters.Get<string[]>("Fields", new[] 
            {"System.AreaPath", "System.TeamProject", "System.IterationPath",
             "System.WorkItemType", "System.State", "System.Reason",
             "System.CreatedDate", "System.CreatedBy", "System.ChangedDate",
             "System.ChangedBy", "System.CommentCount", "System.Title",
             "System.BoardColumn", "System.BoardColumnDone", "Microsoft.VSTS.Common.StateChangeDate",
             "Microsoft.VSTS.Common.Priority", "Microsoft.VSTS.Common.ValueArea", "System.Description",
             "System.Tags" });
            // Where
            Has_Where = Parameters.HasParameter("Where");
            Where = Parameters.Get<string>("Where");
            // TimePrecision
            Has_TimePrecision = Parameters.HasParameter("TimePrecision");
            TimePrecision = Parameters.Get<bool>("TimePrecision");
            // ShowWindow
            Has_ShowWindow = Parameters.HasParameter("ShowWindow");
            ShowWindow = Parameters.Get<bool>("ShowWindow");
            // Deleted
            Has_Deleted = Parameters.HasParameter("Deleted");
            Deleted = Parameters.Get<bool>("Deleted");
            // IncludeLinks
            Has_IncludeLinks = Parameters.HasParameter("IncludeLinks");
            IncludeLinks = Parameters.Get<bool>("IncludeLinks");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetWorkItemController(IProcessUtil processUtil, INodeUtil nodeUtil, TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            ProcessUtil = processUtil;
            NodeUtil = nodeUtil;
            Client = client;
        }
    }
}