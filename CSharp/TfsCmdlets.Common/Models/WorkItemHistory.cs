using System.Runtime.Serialization;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents the history of a given work item
    /// </summary>
    [DataContract]
    public class WorkItemHistoryEntry
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public DateTime ChangedDate { get; set; }
        public string ChangedBy { get; set; }
        public IEnumerable<WorkItemHistoryChangedField> Changes { get; set; }
        public object Comment { get; internal set; }
    }

    [DataContract]
    public class WorkItemHistoryChangedField
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public string ReferenceName { get; set; }
        public object OriginalValue { get; set; }
        public object NewValue { get; set; }
    }
}