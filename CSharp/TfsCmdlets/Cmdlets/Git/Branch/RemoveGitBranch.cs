using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Branch
{
    /// <summary>
    /// Removes from one or more branches from a remote Git repository.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GitBranchStats), SupportsShouldProcess = true)]
    partial class RemoveGitBranch
    {
        /// <summary>
        /// Specifies the name of a branch in the supplied Git repository. Wildcards are supported. 
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        [Alias("RefName")]
        [SupportsWildcards()]
        public object Branch { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter()]
        public object Repository { get; set; }
    }
}