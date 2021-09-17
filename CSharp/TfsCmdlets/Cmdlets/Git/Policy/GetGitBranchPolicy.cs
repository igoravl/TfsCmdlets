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
    public class GetGitBranchPolicy : ProjectLevelCmdlet
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
    }
}
