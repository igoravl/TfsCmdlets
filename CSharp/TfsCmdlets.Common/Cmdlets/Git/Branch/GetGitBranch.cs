using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Git.Branch
{
    /// <summary>
    /// Gets information from one or more branches in a remote Git repository.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsGitBranch", DefaultParameterSetName="Get by name")]
    [OutputType(typeof(GitBranchStats))]
    public class GetGitBranch : CmdletBase
    {
        /// <summary>
        /// Specifies the name of a branch in the supplied Git repository. Wildcards are supported. 
        /// When omitted, all branches are returned.
        /// </summary>
        [Parameter(Position=0, ParameterSetName="Get by name")]
        [ValidateNotNullOrEmpty]
        [Alias("RefName")]
        [SupportsWildcards()]
        public object Branch { get; set; } = "*";

        /// <summary>
        /// Returns the default branch in the given repository.
        /// </summary>
        [Parameter(Mandatory=true, ParameterSetName="Get default")]
        public SwitchParameter Default { get; set; }

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
        protected override void DoProcessRecord()
        {
            WriteItems<GitBranchStats>();
        }
    }

    [Exports(typeof(GitBranchStats))]
    internal partial class GitBranchStatsDataServiceImpl : BaseDataService<GitBranchStats>
    {
        protected override IEnumerable<GitBranchStats> DoGetItems()
        {
            var branch = GetParameter<object>("Branch");
            var defaultBranch = GetParameter<bool>("Default");
            var repo = GetItem<GitRepository>();

            if (repo.Size == 0)
            {
                Logger.Log($"Repository {repo.Name} is empty. Skipping.");
                yield break;
            }

            var client = GetClient<GitHttpClient>();
            string branchName = null;
            bool done = false;

            while (!done) switch(branch)
            {
                case string s when defaultBranch:
                case null:
                {
                    if(repo.DefaultBranch == null)
                    {
                        throw new Exception($"Repository {repo.Name} does not have a default branch set.");
                    }

                    branchName = repo.DefaultBranch.Substring("refs/heads/".Length);
                    done = true;
                    break;
                }
                case string s when string.IsNullOrEmpty(s):
                {
                    throw new ArgumentNullException("Branch", "Branch argument is required.");
                }
                case GitBranchStats gbs:
                {
                    branchName = gbs.Name;
                    done = true;
                    break;
                }
                case string s:
                {
                    branchName = s;
                    done = true;
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid branch '{branch.ToString()}'", "Branch");
                }
            } 

            IEnumerable<GitBranchStats> result;
            
            try
            {
                result = client.GetBranchesAsync(repo.ProjectReference.Name, repo.Id)
                    .GetResult($"Error retrieving branch(es) '{branch}' from repository '{repo.Name}'")
                    .Where(b => b.Name.IsLike(branchName));
            }
            catch(Exception ex)
            {
                if(ex.InnerException?.Message?.StartsWith("VS403403")?? false)
                {
                    result = new List<GitBranchStats>();
                }
                else
                {
                    throw;
                }
            }

            foreach(var b in result)
            {
                yield return b;
            }
        }
    }
}