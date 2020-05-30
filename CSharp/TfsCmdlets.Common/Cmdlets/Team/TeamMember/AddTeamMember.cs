using System.Management.Automation;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    [Cmdlet(VerbsCommon.Add, "TeamMember", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(TeamAdmins))]
    public class AddTeamMember : BaseCmdlet
    {
        /*
                # Specifies the board name(s). Wildcards accepted
                [Parameter(Position=0)]
                [Alias("Name")]
                [Alias("Member")]
                [Alias("User")]
                public object Identity { get; set; }

                [Parameter(ValueFromPipeline=true)]
                public object Team { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team "{Team}"."}; if(t.ProjectName) {Project = t.ProjectName}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)

                gi = Get-TfsIdentity -Identity t.Id -Collection tpc
                ui = Get-TfsIdentity -Identity Identity -Collection tpc

                if(! ui)
                {
                    throw new Exception($"Invalid or non-existent identity "{Identity}"")
                }

                var client = tpc.GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>();

                this.Log($"Adding {{ui}.IdentityType} "$(ui.DisplayName) ($(ui.Properties["Account"]))" to team "$(t.Name)"");

                if(! ShouldProcess(t.Name, $"Add member "{{ui}.DisplayName} ($(ui.Properties["Account"]))""))
                {
                    return
                }

                task = client.AddMemberToGroupAsync(gi.Descriptor, ui.Descriptor); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error adding team member "{{ui}.DisplayName}" to team "$(t.Name)"" task.Exception.InnerExceptions })
            }
        }
        */
    }
}
