//HintName: TfsCmdlets.Cmdlets.Git.Policy.GetGitPolicyType.g.cs
namespace TfsCmdlets.Cmdlets.Git.Policy
{
    [Cmdlet("Get", "TfsGitPolicyType")]
    [OutputType(typeof(Microsoft.TeamFoundation.Policy.WebApi.PolicyType))]
    public partial class GetGitPolicyType: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter()]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}