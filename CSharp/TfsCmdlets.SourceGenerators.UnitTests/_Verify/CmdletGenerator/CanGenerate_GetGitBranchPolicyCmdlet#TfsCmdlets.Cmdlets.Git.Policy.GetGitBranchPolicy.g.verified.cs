//HintName: TfsCmdlets.Cmdlets.Git.Policy.GetGitBranchPolicy.g.cs
namespace TfsCmdlets.Cmdlets.Git.Policy
{
    [Cmdlet("Get", "TfsGitBranchPolicy")]
    [OutputType(typeof(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration))]
    public partial class GetGitBranchPolicy: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
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