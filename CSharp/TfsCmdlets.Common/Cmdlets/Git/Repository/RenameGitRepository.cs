/*
.SYNOPSIS
Renames a Git repository in a team project.

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
Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository
System.String
*/

using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    [Cmdlet(VerbsCommon.Rename, "GitRepository", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(GitRepository))]
    public class RenameGitRepository : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true, ValueFromPipeline=true, Position=0)]
        public object Repository,

                [Parameter(Mandatory=true, Position=1)]
                [string] 
                NewName,

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Core.WebApi"
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.SourceControl.WebApi"
            }

            protected override void ProcessRecord()
            {
                if (Repository is Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository)
                {
                    Project = Repository.ProjectReference.Name
                }

                tp = this.GetProject();
                #tpc = tp.Store.TeamProjectCollection

                var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

                if (Repository is Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository)
                {
                    reposToRename = @(Repository)
                }
                else
                {
                    reposToRename = Get-TfsGitRepository -Name Repository -Project Project -Collection Collection
                }

                foreach(repo in reposToRename)
                {
                    if (ShouldProcess(repo.Name, $"Rename Git repository in Team Project {{tp}.Name} to NewName"))
                    {
                        task = client.RenameRepositoryAsync(repo, NewName)
                        task.Wait()

                        if (Passthru)
                        {
                            WriteObject(task.Result); return;
                        }
                    }
                }
            }
        }

        */
    }
}
