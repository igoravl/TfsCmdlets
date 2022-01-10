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
        public string Commit { get; set; }

        /* ParameterSetName: Search commits */

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public string Author { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public string Committer { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public GitVersionDescriptor CompareVersion { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public string FromCommit { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public string ItemPath { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public GitVersionDescriptor ItemVersion { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public string ToCommit { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(ParameterSetName = "Search commits")]
        public DateTime ToDate { get; set; }

        /* Shared Parameters */

        /// <summary>
        /// </summary>
        [Parameter()]
        public bool ExcludeDeletes { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public bool IncludeLinks { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public bool IncludePushData { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public bool IncludeUserImageUrl { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public bool ShowOldestCommitsFirst { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public int Skip { get; set; }

        /// <summary>
        /// </summary>
        [Parameter()]
        public int Top { get; set; }
 
        /// <summary>
        /// Specifies the name or ID of a Git repository. Wildcards are supported. 
        /// When omitted, all Git repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Repository { get; set; }
   }
}