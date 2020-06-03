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
    public class GetGitBranchPolicy : BaseCmdlet<PolicyConfiguration>
    {
        [Parameter(Position = 0)]
        public object PolicyType { get; set; } = "*";

        [Parameter(ValueFromPipeline = true)]
        [Alias("RefName")]
        public object Branch { get; set; } = "master";

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
    }

    [Exports(typeof(PolicyConfiguration))]
    internal class GitBranchPolicyDataServiceImpl : BaseDataService<PolicyConfiguration>
    {
        protected override IEnumerable<PolicyConfiguration> DoGetItems(object userState)
        {
            var repo = this.GetInstanceOf<GitRepository>();
            OverrideParameter("Project", repo.ProjectReference.Name);

            var branch = $"refs/heads/{GetInstanceOf<GitBranchStats>().Name}";
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
