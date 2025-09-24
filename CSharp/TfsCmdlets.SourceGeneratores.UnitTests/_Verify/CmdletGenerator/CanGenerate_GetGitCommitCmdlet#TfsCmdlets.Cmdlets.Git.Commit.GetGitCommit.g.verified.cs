//HintName: TfsCmdlets.Cmdlets.Git.Commit.GetGitCommit.g.cs
namespace TfsCmdlets.Cmdlets.Git.Commit
{
    [Cmdlet("Get", "TfsGitCommit", DefaultParameterSetName = "Search commits")]
    [OutputType(typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitCommitRef))]
    public partial class GetGitCommit: CmdletBase
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