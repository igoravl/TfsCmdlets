using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Creates a new team.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsTeam", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeam))]
    public class NewTeam : NewCmdletBase<Models.Team>
    {
        /// <summary>
        /// Specifies the name of the new team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Team { get; set; }

        /// <summary>
        /// Specifies a description of the new team.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }
    }

    partial class TeamDataService
    {
        protected override Models.Team DoNewItem()
        {
            var (_, tp) = GetCollectionAndProject();
            var team = GetParameter<string>(nameof(NewTeam.Team));
            var description = GetParameter<string>(nameof(NewTeam.Description));

            if (!ShouldProcess(tp, $"Create team {team}"))
            {
                return null;
            }

            return GetClient<TeamHttpClient>().CreateTeamAsync(new WebApiTeam() {
                Name = team,
                Description = description,
            }, tp.Name).GetResult($"Error creating team {team}");
        }
    }
}