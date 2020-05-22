using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    [Cmdlet(VerbsCommon.Remove, "TeamMember", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveTeamMember : BaseCmdlet
    {
        /*
                # Specifies the board name(s). Wildcards accepted
                [Parameter(Position=0,ValueFromPipeline=true)]
                [Alias("Name")]
                [Alias("User")]
                [Alias("Member")]
                public object Identity { get; set; }

                [Parameter()]
                public object Team { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

            protected override void ProcessRecord()
            {
                if(Identity.TeamId && Identity.ProjectId)
                {
                    Project = Identity.ProjectId 
                    t = Get-TfsTeam -Team Identity.TeamId -Project Project -Collection Collection

                    tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)
                }
                else
                {
                    t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team "{Team}"."}; if(t.ProjectName) {Project = t.ProjectName}; tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)
                }

                gi = Get-TfsIdentity -Identity t.Id -Collection tpc
                ui = Get-TfsIdentity -Identity Identity -Collection tpc

                if(! ui)
                {
                    throw new Exception($"Invalid or non-existent identity "{Identity}"")
                }

                client = Get-TfsRestClient "Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient" -Collection tpc

                this.Log($"Removing {{ui}.IdentityType} "$(ui.DisplayName) ($(ui.Properties["Account"]))" from team "$(t.Name)"");

                if(! ShouldProcess(t.Name, $"Remove member "{{ui}.DisplayName} ($(ui.Properties["Account"]))""))
                {
                    return
                }

                task = client.RemoveMemberFromGroupAsync(gi.Descriptor, ui.Descriptor); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error removing team member "{{ui}.DisplayName}" from team "$(t.Name)"" task.Exception.InnerExceptions })
            }
        }
        */
    }
}
