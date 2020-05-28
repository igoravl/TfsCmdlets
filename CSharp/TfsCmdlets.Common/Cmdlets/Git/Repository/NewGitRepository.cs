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
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    [Cmdlet(VerbsCommon.New, "GitRepository", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(GitRepository))]
    public class NewGitRepository : BaseCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Name")]
        public string Repository { get; set; }

        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {
            if (!ShouldProcess(Repository, "Create Git repository")) return;

            var (tpc, tp) = this.GetCollectionAndProject();
            var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

            var tpRef = new TeamProjectReference()
            {
                Id = tp.Id,
                Name = tp.Name
            };

            var repoToCreate = new GitRepository()
            {
                Name = Repository,
                ProjectReference = tpRef
            };

            var result = client.CreateRepositoryAsync(repoToCreate, tp.Name).GetResult("Error create Git repository");

            if (Passthru)
            {
                WriteObject(result);
            }
        }
    }
}