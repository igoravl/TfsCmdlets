using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Deletes a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTeam))]
    partial class RemoveTeam 
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Team { get; set; }
    }

    [CmdletController(typeof(Models.Team))]
    partial class RemoveTeamController
    {
        protected override IEnumerable Run()
        {
            var client = Data.GetClient<TeamHttpClient>();

            foreach (var team in Items)
            {
                if (!PowerShell.ShouldProcess($"[Project: {team.ProjectName}]/[Team: {team.Name}]", $"Delete team")) continue;

                client.DeleteTeamAsync(team.ProjectName, team.Name)
                    .Wait($"Error deleting team {team.Name}");
            }

            return null;
        }
    }
}