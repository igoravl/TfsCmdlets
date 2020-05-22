using System.Management.Automation;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    [Cmdlet(VerbsCommon.Remove, "TeamAdmin", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(TeamAdmins))]
    public class RemoveTeamAdmin : BaseCmdlet
    {
        /*
                # Specifies the board name(s). Wildcards accepted
                [Parameter(Position=0,ValueFromPipeline=true)]
                [Alias("Name")]
                [Alias("User")]
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

                id = Get-TfsIdentity -Identity Identity -Collection tpc

                client = Get-TfsRestClient "TfsCmdlets.TeamAdminHttpClient" -Collection tpc

                this.Log($"Removing {{id}.IdentityType} "$(id.DisplayName) ($(id.Properties["Account"]))" from team "$(t.Name)"");

                if(! ShouldProcess(t.Name, $"Remove administrator "{{id}.DisplayName} ($(id.Properties["Account"]))""))
                {
                    return
                }

                if(! ([bool] client.RemoveTeamAdmin(tp.Name, t.Id, id.Id).success))
                {
                    throw new Exception("Error removing team administrator")
                }
            }
        }
        */
    }
}
