//HintName: TfsCmdlets.Cmdlets.Git.Branch.GetGitBranch.g.cs
namespace TfsCmdlets.Cmdlets.Git.Branch
{
    [Cmdlet("Get", "TfsGitBranch", DefaultParameterSetName = "Get by name")]
    [OutputType(typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats))]
    public partial class GetGitBranch: CmdletBase
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