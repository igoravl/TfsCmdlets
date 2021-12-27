using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiIdentityRef = Microsoft.VisualStudio.Services.WebApi.IdentityRef;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Controllers.WorkItem.History
{
    [CmdletController(typeof(Models.WorkItemHistoryEntry))]
    partial class GetWorkItemHistoryController
    {
        private static readonly string[] _IgnoredFields = new[] {
            "System.Rev", "System.AuthorizedDate", "System.RevisedDate", 
            "System.CreatedBy", "System.ChangedDate", "System.ChangedBy", 
            "System.AuthorizedAs"};

        public override IEnumerable<Models.WorkItemHistoryEntry> Invoke()
        {
            var wi = Data.GetItem<WebApiWorkItem>();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            var revisions = client.GetRevisionsAsync((int)wi.Id, (int)wi.Rev, expand: WorkItemExpand.Fields)
                .GetResult("Error retrieving work item revisions");

            WebApiWorkItem previous = null;
            List<Models.WorkItemHistoryEntry> history = new List<Models.WorkItemHistoryEntry>();

            var iRef = new WebApiIdentityRef();
            var iW = (Models.IdentityRefWrapper) iRef;

            foreach(var rev in revisions.OrderBy(r => r.Rev))
            {
                history.Add(new Models.WorkItemHistoryEntry()
                {
                    Id = (int)rev.Id,
                    Revision = (int) rev.Rev,
                    ChangedDate = rev.GetField<DateTime>("System.ChangedDate"),
                    ChangedBy = rev.GetField<Models.IdentityRefWrapper>("System.ChangedBy")?.DisplayName,
                    Comment = rev.GetField<string>("System.History"),
                    Changes = rev.Fields
                        .Where(f => !_IgnoredFields.Contains(f.Key) && IsChangedInRevision(f.Key, rev, previous))
                        .Select(f => new Models.WorkItemHistoryChangedField()
                    {
                        Id = (int)rev.Id,
                        Revision = (int)rev.Rev,
                        ReferenceName = f.Key,
                        OriginalValue = previous?.GetField(f.Key),
                        NewValue = f.Value
                    }).ToList()
                });

                previous = rev;
            }

            return history;
        }

        private bool IsChangedInRevision(string referenceName, WebApiWorkItem current, WebApiWorkItem previous)
            => !object.Equals(current.GetField(referenceName), previous?.GetField(referenceName));
    }
}
