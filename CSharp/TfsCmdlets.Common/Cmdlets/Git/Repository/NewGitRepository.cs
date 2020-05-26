/*
.SYNOPSIS
Creates a new Git repository in a team project.

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
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String
*/

using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    [Cmdlet(VerbsCommon.New, "GitRepository", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(GitRepository))]
    public class NewGitRepository : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true)]
                [Alias("Name")]
                [string] 
                Repository,

                [Parameter(ValueFromPipeline=true)]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void BeginProcessing()
            {
                Add-Type -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
                Add-Type -AssemblyName "Microsoft.TeamFoundation.SourceControl.WebApi"
                Add-Type -AssemblyName "Microsoft.TeamFoundation.Common"
            }

            protected override void ProcessRecord()
            {
                if(ShouldProcess(Repository, "Create Git repository"))
                {
                    tp = Get-TfsTeamProject -Project Project -Collection Collection
                    #tpc = tp.Store.TeamProjectCollection

                    client = Get-TfsRestClient "Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient" -Collection tpc
                    tpRef = [Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference] @{Id = tp.Guid; Name = tp.Name}
                    repoToCreate = [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository] @{Name = Repository; ProjectReference = tpRef}
                    task = client.CreateRepositoryAsync(repoToCreate, tp.Name)

                    result = task.Result

                    if (Passthru)
                    {
                        WriteObject(result); return;
                    }
                }
            }
        }
        */
    }
}
