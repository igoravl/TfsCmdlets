//HintName: TfsCmdlets.Cmdlets.Git.Branch.RemoveGitBranch.g.cs
namespace TfsCmdlets.Cmdlets.Git.Branch
{
    [Cmdlet("Remove", "TfsGitBranch", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats))]
    public partial class RemoveGitBranch: CmdletBase
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