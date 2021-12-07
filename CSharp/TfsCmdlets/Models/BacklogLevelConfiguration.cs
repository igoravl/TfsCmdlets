using System.Management.Automation;
using WebApiBacklogLevelConfiguration = Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the backlog level configuration object
    /// </summary>
    public class BacklogLevelConfiguration : PSObject
    {
        private WebApiBacklogLevelConfiguration InnerObject => (WebApiBacklogLevelConfiguration)BaseObject;

        internal BacklogLevelConfiguration(WebApiBacklogLevelConfiguration b,
            string project, string team) : base(b)
        {
            this.AddNoteProperty(nameof(Project), project);
            this.AddNoteProperty(nameof(Team), team);
        }

        internal string Id => InnerObject.Id;

        internal string Name => InnerObject.Name;

        internal string Project => (string) this.GetProperty(nameof(Project)).Value;

        internal string Team => (string) this.GetProperty(nameof(Team)).Value;

   }
}