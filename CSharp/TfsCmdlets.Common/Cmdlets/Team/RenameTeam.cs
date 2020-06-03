/*
.SYNOPSIS
Renames a team.

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

.INPUTS
Microsoft.TeamFoundation.Client.TeamFoundationTeam
System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team
{
    [Cmdlet(VerbsCommon.Rename, "TfsTeam", ConfirmImpact = ConfirmImpact.Medium)]
    //[OutputType(typeof(Microsoft.TeamFoundation.Client.TeamFoundationTeam))]
    public class RenameTeam : BaseCmdlet
    {
        /*
                [Parameter(Position=0, ValueFromPipeline=true)]
                [Alias("Name")]
                [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.Client.TeamFoundationTeam])}) 
                [SupportsWildcards()]
                public object Team { get; set; } = "*";

                [Parameter()]
                public string NewName { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                result = Set-TfsTeam -Team Team -NewName NewName -Project Project -Collection Collection

                if (Passthru)
                {
                    WriteObject(result); return;
                }
            }
        }
        */
    }
}
