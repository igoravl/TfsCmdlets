using System.Management.Automation;
using WebApiBacklogLevelConfiguration = Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the backlog level configuration object
    /// </summary>
    public class BacklogLevelConfiguration : ModelBase<WebApiBacklogLevelConfiguration>
    {
        public BacklogLevelConfiguration(WebApiBacklogLevelConfiguration b, string project, string team)
            : base(b)
        {
            this.AddNoteProperty(nameof(Project), project);
            this.AddNoteProperty(nameof(Team), team);
        }

        public string Id => InnerObject.Id;

        public string Name => InnerObject.Name;

        public string Project => (string)this.GetProperty(nameof(Project)).Value;

        public string Team => (string)this.GetProperty(nameof(Team)).Value;

    }
}