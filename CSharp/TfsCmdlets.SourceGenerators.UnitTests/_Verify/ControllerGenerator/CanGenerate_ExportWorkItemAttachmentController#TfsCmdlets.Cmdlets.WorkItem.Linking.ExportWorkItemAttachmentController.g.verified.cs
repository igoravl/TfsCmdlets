//HintName: TfsCmdlets.Cmdlets.WorkItem.Linking.ExportWorkItemAttachmentController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    internal partial class ExportWorkItemAttachmentController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // Attachment
        protected bool Has_Attachment { get; set; }
        protected object Attachment { get; set; }
        // WorkItem
        protected bool Has_WorkItem { get; set; }
        protected object WorkItem { get; set; }
        // Destination
        protected bool Has_Destination { get; set; }
        protected string Destination { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
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
        protected IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation> Items => Attachment switch {
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation);
        protected override void CacheParameters()
        {
            // Attachment
            Has_Attachment = Parameters.HasParameter("Attachment");
            Attachment = Parameters.Get<object>("Attachment", "*");
            // WorkItem
            Has_WorkItem = Parameters.HasParameter("WorkItem");
            WorkItem = Parameters.Get<object>("WorkItem");
            // Destination
            Has_Destination = Parameters.HasParameter("Destination");
            Destination = Parameters.Get<string>("Destination");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public ExportWorkItemAttachmentController(TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}