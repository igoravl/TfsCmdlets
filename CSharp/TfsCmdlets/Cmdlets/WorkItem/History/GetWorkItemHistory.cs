using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.History
{
    /// <summary>
    /// Gets the history of changes of a work item.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, NoAutoPipeline = true, OutputType = typeof(Models.WorkItemHistoryEntry))]
    partial class GetWorkItemHistory
    {
        /// <summary>
        /// The work item to retrieve the history for.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }
    }

    [CmdletController(typeof(Models.WorkItemHistoryEntry), Client=typeof(IWorkItemTrackingHttpClient))]
    partial class GetWorkItemHistoryController
    {
        private static readonly string[] _IgnoredFields = new[] {
            "System.Rev", "System.AuthorizedDate", "System.RevisedDate",
            "System.CreatedBy", "System.ChangedDate", "System.ChangedBy",
            "System.AuthorizedAs"};

        protected override IEnumerable Run()
        {
            var workItems = GetItems<WebApiWorkItem>();

            foreach (var workItem in workItems)
            {
                var revisions = Client.GetRevisionsAsync((int)workItem.Id, (int)workItem.Rev, expand: WorkItemExpand.All)
                    .GetResult("Error retrieving work item revisions");

                WebApiWorkItem previous = null;

                foreach (var rev in revisions.OrderBy(r => r.Rev))
                {
                    yield return new Models.WorkItemHistoryEntry()
                    {
                        Id = (int)rev.Id,
                        Revision = (int)rev.Rev,
                        ChangedDate = rev.GetField<DateTime>("System.ChangedDate"),
                        ChangedBy = new Models.IdentityRefWrapper(rev.GetField<WebApiIdentityRef>("System.ChangedBy")).DisplayName,
                        Comment = rev.GetField<string>("System.History"),
                        Changes = rev.Fields
                            .Where(f => !_IgnoredFields.Contains(f.Key) && IsChangedInRevision(f.Key, rev, previous))
                            .Select(f => new Models.WorkItemHistoryChangedField()
                            {
                                ReferenceName = f.Key,
                                OriginalValue = previous?.GetField(f.Key),
                                NewValue = f.Value,
                                Id = (int)rev.Id,
                                Revision = (int)rev.Rev,
                            }).ToList()
                    };

                    previous = rev;
                }
            }
        }

        private bool IsChangedInRevision(string referenceName, WebApiWorkItem current, WebApiWorkItem previous)
            => !object.Equals(current.GetField(referenceName), previous?.GetField(referenceName));
    }
}