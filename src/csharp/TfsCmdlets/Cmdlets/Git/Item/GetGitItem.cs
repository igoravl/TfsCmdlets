using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Item
{
    /// <summary>
    /// Gets information from one or more items (folders and/or files) in a remote Git repository.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, NoAutoPipeline = true, DefaultParameterSetName = "Get by commit SHA", OutputType = typeof(GitItem))]
    partial class GetGitItem
    {
        /// <summary>
        /// Specifies the path to items (folders and/or files) in the supplied Git repository. Wildcards are supported. 
        /// When omitted, all items in the root of the Git repository are retrieved.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Path")]
        [SupportsWildcards]
        public object Item { get; set; } = "/*";

        [Parameter(ParameterSetName = "Get by commit SHA")]
        public object Commit { get; set; }

        [Parameter(ParameterSetName = "Get by tag", Mandatory = true)]
        public string Tag { get; set; }

        [Parameter(ParameterSetName = "Get by branch", Mandatory = true)]
        public string Branch { get; set; }

        [Parameter]
        public SwitchParameter IncludeContent { get; set; }

        [Parameter]
        public SwitchParameter IncludeMetadata { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Repository { get; set; }
    }
}