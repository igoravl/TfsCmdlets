/*

.SYNOPSIS
    Creates a new team.

.PARAMETER Project
    Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Passthru
    Returns the results of the command. By default, this cmdlet does not generate any output. 

.INPUTS
    System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team
{
    [Cmdlet(VerbsCommon.New, "Team", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.WebApiTeam))]
    public class NewTeam : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true, ValueFromPipeline=true)]
                [Alias("Name")]
                [string] 
                Team,

                [Parameter()]
                [string] 
                Description,

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void BeginProcessing()
            {

            }

            protected override void ProcessRecord()
            {
                if (! ShouldProcess(Project, $"Create team {Team}"))
                {
                    return
                }

                tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                client = Get-TfsRestClient "Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient" -Collection tpc

                result = client.CreateTeamAsync((new Microsoft.TeamFoundation.Core.WebApi.WebApiTeam() -Property @{
                    Name = Team
                    Description = Description
                }), tp.Name).Result

                if (Passthru)
                {
                    WriteObject(result); return;
                }
            }
        }
        */
    }
}
