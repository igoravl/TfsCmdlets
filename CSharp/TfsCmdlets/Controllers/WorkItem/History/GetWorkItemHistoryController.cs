using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
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

        protected override IEnumerable Run()
        {
            var client = Data.GetClient<WorkItemTrackingHttpClient>();
            var workItems = GetItems<WebApiWorkItem>();

            foreach (var workItem in workItems)
            {
                var revisions = client.GetRevisionsAsync((int)workItem.Id, (int)workItem.Rev, expand: WorkItemExpand.All)
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
