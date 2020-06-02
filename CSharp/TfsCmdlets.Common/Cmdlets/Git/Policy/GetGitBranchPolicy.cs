using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Git.Policy
{
    /// <summary>
    /// Gets information about the Git branch policies defined for the given branches.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "GitBranchPolicy")]
    [OutputType(typeof(PolicyConfiguration))]
    public class GetGitBranchPolicy : BaseCmdlet<PolicyConfiguration>
    {
        [Parameter()]
        public object PolicyType { get; set; } = "*";

        [Parameter()]
        [Alias("RefName")]
        [SupportsWildcards]
        [AllowNull()]
        public object Branch { get; set; } = "master";

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        [SupportsWildcards()]
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
            throw new NotImplementedException();
            
            var repo = this.GetInstanceOf<GitRepository>();
            OverrideParameter("Project", repo.ProjectReference.Name);

            var branch = GetInstanceOf<GitBranchStats>();
            var policyType = GetInstanceOf<PolicyType>();

            // if(PolicyType != null)
            // {
            //     policy = GetPolicyType -Type PolicyType -Project tp -Collection tpc

            //     if(! policy)
            //     {
            //         throw new Exception($"Invalid or non-existent policy type "{PolicyType}"")
            //     }

            //     policyTypeId = PolicyType.Id
            // }

            // repos = GetGitRepository -Repository Repository -Project tp -Collection tpc

            // foreach(repo in repos)
            // {
            //     task = client.GetPolicyConfigurationsAsync(tp.Name, repo.Id, branch, policyTypeId); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving branch policy configurations for repository "{{repo}.Name}"" task.Exception.InnerExceptions })
            // }

            // WriteObject(result.PolicyConfigurations); return;
        }
    }
}
