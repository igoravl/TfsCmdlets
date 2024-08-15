using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Adds a link between two work items.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection)]
    partial class AddWorkItemLink
    {
        /// <summary>
        /// Specifies the work item to link from.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("Id", "From")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Specifies the work item to link to.
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, ParameterSetName = "Link to work item")]
        [Alias("To")]
        [ValidateNotNull()]
        public object TargetWorkItem { get; set; }

        /// <summary>
        /// Specifies the type of the link to create.
        /// </summary>
        [Parameter(Position = 2, Mandatory = true, ParameterSetName = "Link to work item")]
        [Alias("EndLinkType", "Type")]
        public WorkItemLinkType LinkType { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        /// <value></value>
        [Parameter]
        public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Bypasses any rule validation when saving the work item. Use it with caution, as this 
        /// may leave the work item in an invalid state.
        /// </summary>
        [Parameter]
        public SwitchParameter BypassRules { get; set; }

        /// <summary>
        /// Do not fire any notifications for this change. Useful for bulk operations and automated processes.
        /// </summary>
        [Parameter]
        public SwitchParameter SuppressNotifications { get; set; }

        /// <summary>
        /// Defines a comment to add to the link.
        /// </summary>
        [Parameter]
        public string Comment { get; set; }
    }

    [CmdletController(typeof(WebApiWorkItemRelation), Client=typeof(IWorkItemTrackingHttpClient))]
    partial class AddWorkItemLinkController
    {
        [Import]
        private IKnownWorkItemLinkTypes KnownLinkTypes { get; set; }

        protected override IEnumerable Run()
        {
            {
                var sourceWi = Data.GetItem<WebApiWorkItem>();
                var targetWi = Data.GetItem<WebApiWorkItem>(new { WorkItem = Parameters.Get<object>(nameof(AddWorkItemLink.TargetWorkItem)) });
                var linkType = Parameters.Get<WorkItemLinkType>(nameof(AddWorkItemLink.LinkType));
                var runId = Guid.NewGuid();

                var patch = new JsonPatchDocument() {
                   new JsonPatchOperation() {
                        Operation = Operation.Test,
                        Path = "/rev",
                        Value = sourceWi.Rev
                   },
                   new JsonPatchOperation() {
                        Operation = Operation.Add,
                        Path = "/relations/-",
                        Value = new WebApiWorkItemRelation() {
                            Rel = KnownLinkTypes.GetReferenceName(linkType),
                            Url = targetWi.Url,
                            Attributes = new Dictionary<string,object>() {
                                ["comment"] = Parameters.Get<string>(nameof(AddWorkItemLink.Comment), string.Empty)
                            }
                        }
                   }
                };

                var result = Client.UpdateWorkItemAsync(patch, (int)sourceWi.Id, 
                        bypassRules: BypassRules, 
                        suppressNotifications: SuppressNotifications)
                    .GetResult("Error updating target work item");

                return result.Relations.Where(r => 
                    r.Url == targetWi.Url && 
                    r.Rel == KnownLinkTypes.GetReferenceName(linkType)
                ).ToList();
            }
        }
    }
}