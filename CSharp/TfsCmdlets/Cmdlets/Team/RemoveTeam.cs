using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Deletes a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeam", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeam))]
    public class RemoveTeam : RemoveCmdletBase<Models.Team>
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Team { get; set; }
    }

    partial class TeamDataService
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoRemoveItem()
        {
            var (_, tp) = GetCollectionAndProject();
            var teams = GetItems<Models.Team>();

            foreach (var t in teams)
            {
                if (!ShouldProcess(tp, $"Delete team '{t.Name}'"))
                {
                    continue;
                }

                GetClient<TeamHttpClient>().DeleteTeamAsync(tp.Name, t.Name)
                    .Wait($"Error deleting team {t.Name}");
            }
        }
    }
}