/*
.SYNOPSIS
    Gets information from one or more Git repositories in a team project.

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
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
*/

using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    [Cmdlet(VerbsCommon.Get, "GitRepository")]
    [OutputType(typeof(GitRepository))]
    public class GetGitRepository : BaseCmdlet
    {

        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; } = "*";

        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

        protected override void ProcessRecord()
        {
            var tpc = this.GetCollection();
            var tp = this.GetProject();
            object result = null;

            while (result == null)
            {
                switch (Repository)
                {
                    case PSObject o:
                        {
                            Repository = o.BaseObject;
                            continue;
                        }
                    case GitRepository repo:
                        {
                            result = repo;
                            break;
                        }
                    case Guid guid:
                        {
                            var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
                            result = client.GetRepositoryAsync(tp.Name, guid).GetResult($"Error getting repository with ID {guid}");
                            break;
                        }
                    case string s when s.IsGuid():
                        {
                            Repository = new Guid(s);
                            continue;
                        }
                    case string s when !s.IsWildcard():
                        {
                            var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
                            result = client.GetRepositoryAsync(tp.Name, s).GetResult($"Error getting repository '{s}'");
                            break;
                        }
                    case string s:
                        {
                            var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
                            result = client.GetRepositoriesAsync(tp.Name).GetResult($"Error getting repository(ies) '{s}'").Where(r => r.Name.IsLike(s));
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException(nameof(Repository));
                        }
                }
            }

            WriteObject(result, true);
        }
    }
}