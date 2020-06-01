/*
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
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git.Branch
{
    /// <summary>
    /// Gets information from one or more branches in a remote Git repository.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "GitBranch")]
    [OutputType(typeof(GitBranchStats))]
    public class GetGitBranch : BaseCmdlet
    {
        /// <summary>
        /// Specifies the name of a branch in the supplied Git repository. Wildcards are supported. 
        /// When omitted, all branches are returned.
        /// </summary>
        /// <value></value>
        [Parameter()]
        [Alias("RefName")]
        [SupportsWildcards()]
        public object Branch { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Repository { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var repo = this.GetOne<GitRepository>();
            var tpc = this.GetCollection();
            var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

            if (repo.Size == 0)
            {
                this.Log($"Repository {repo.Name} is empty. Skipping.");
                return;
            }

            string branch;

            switch(Branch)
            {
                case null: 
                case string s when string.IsNullOrEmpty(s):
                {
                    throw new ArgumentNullException(nameof(Branch));
                }
                case GitBranchStats gbs:
                {
                    branch = gbs.Name;
                    break;
                }
                case string s:
                {
                    branch = s;
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid value '{Branch.ToString()}' for argument Branch");
                }
            } 

            var result = client.GetBranchesAsync(repo.ProjectReference.Name, repo.Id)
                .GetResult($"Error retrieving branch(es) '{branch}' from repository '{repo.Name}'")
                .Where(b => b.Name.IsLike(branch));

            foreach (var b in result)
            {
                var pso = new PSObject(b);
                pso.AddNoteProperty("Project", repo.ProjectReference.Name);
                pso.AddNoteProperty("Repository", repo.Name);
                WriteObject(pso);
            }
        }
    }
}