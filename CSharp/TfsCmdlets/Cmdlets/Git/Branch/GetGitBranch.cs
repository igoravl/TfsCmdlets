using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Branch
{
    /// <summary>
    /// Gets information from one or more branches in a remote Git repository.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, NoAutoPipeline = true, DefaultParameterSetName = "Get by name", 
     OutputType = typeof(GitBranchStats))]
    partial class GetGitBranch
    {
        /// <summary>
        /// Specifies the name of a branch in the supplied Git repository. Wildcards are supported. 
        /// When omitted, all branches are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by name")]
        [ValidateNotNullOrEmpty]
        [Alias("RefName")]
        [SupportsWildcards()]
        public object Branch { get; set; } = "*";

        /// <summary>
        /// Returns the default branch in the given repository.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get default")]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Repository { get; set; }
    }
}