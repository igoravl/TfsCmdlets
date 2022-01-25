using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Gets information from one or more Git commits in a remote repository.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, NoAutoPipeline = true, DefaultParameterSetName = "Search commits", OutputType = typeof(GitCommitRef))]
    partial class GetGitCommit
    {
        /* ParameterSetName: Get by commit SHA */

        [Parameter(ParameterSetName = "Get by commit SHA", Mandatory = true, Position = 0)]
        public object Commit { get; set; }

        /* ParameterSetName: Get by tag */

        [Parameter(ParameterSetName = "Get by tag", Mandatory = true)]
        public string Tag { get; set; }

        /* ParameterSetName: Get by branch */

        [Parameter(ParameterSetName = "Get by branch", Mandatory = true)]
        public string Branch { get; set; }

        /* ParameterSetName: Search commits */

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string Author { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string Committer { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public GitVersionDescriptor CompareVersion { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string FromCommit { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string ItemPath { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string ToCommit { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public DateTime ToDate { get; set; }

        /* Shared Parameters */

        /// <summary>
        /// </summary>
        [Parameter()]
        public SwitchParameter ExcludeDeletes { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeLinks { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludePushData { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeUserImageUrl { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public SwitchParameter ShowOldestCommitsFirst { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public int Skip { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public int Top { get; set; }
 
        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Repository { get; set; }
   }
}