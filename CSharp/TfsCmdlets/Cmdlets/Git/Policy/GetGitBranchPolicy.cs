using System;
using System.Linq;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Git.Policy
{
    /// <summary>
    /// Gets the Git branch policy configuration of the given Git branches.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsGitBranchPolicy")]
    [OutputType(typeof(PolicyConfiguration))]
    public class GetGitBranchPolicy : CmdletBase
    {
        /// <summary>
        /// Specifies the policy type of the branch policy to return. Wildcards are supported. 
        /// When omitted, all branch policies defined for the given branch are returned.
        /// </summary>
        [Parameter(Position = 0)]
        public object PolicyType { get; set; } = "*";

        /// <summary>
        /// Specifies the name of the branch to query for branch policies. When omitted, 
        /// the default branch in the given repository is queried.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        [Alias("RefName")]
        public object Branch { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter()]
        public object Repository;

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
            WriteItems<PolicyConfiguration>();
        }
    }

    [Exports(typeof(PolicyConfiguration))]
    internal class GitBranchPolicyDataServiceImpl : BaseDataService<PolicyConfiguration>
    {
        protected override IEnumerable<PolicyConfiguration> DoGetItems()
        {
            var repo = this.GetItem<GitRepository>();

            var branch = $"refs/heads/{GetItem<GitBranchStats>().Name}";
            var policyType = GetParameter<object>("PolicyType");

            while(true) switch(policyType)
            {
                case PolicyType pt:
                {
                    policyType = pt.Id;
                    continue;
                }
                case string s when s.IsGuid():
                {
                    policyType = new Guid(s);
                    continue;
                }
                case Guid g:
                {
                    foreach(var pol in GetClient<GitHttpClient>()
                        .GetPolicyConfigurationsAsync(repo.ProjectReference.Name, repo.Id, branch, g)
                        .GetResult($"Error getting policy definitions from branch {branch} in repository {repo.Name}")
                        .PolicyConfigurations)
                    {
                        yield return pol;
                    }

                    yield break;
                }
                case string s:
                {
                    foreach(var pol in GetClient<GitHttpClient>()
                        .GetPolicyConfigurationsAsync(repo.ProjectReference.Name, repo.Id, branch)
                        .GetResult($"Error getting policy definitions from branch {branch} in repository {repo.Name}")
                        .PolicyConfigurations
                        .Where(pc => pc.Type.DisplayName.IsLike(s)))
                    {
                        yield return pol;
                    }

                    yield break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid policy type '{policyType}'");
                }
            }
        }
    }
}
