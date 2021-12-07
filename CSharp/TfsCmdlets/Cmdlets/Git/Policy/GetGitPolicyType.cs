using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Policy
{
    /// <summary>
    /// Gets one or more Git branch policies supported by the given team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsGitPolicyType")]
    [OutputType(typeof(PolicyType))]
    partial class GetGitPolicyType
    {
        /// <summary>
        /// Specifies the display name or ID of the policy type. Wildcards are supported.
        /// When omitted, all policy types supported by the given team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object PolicyType { get; set; } = "*";
    }
}