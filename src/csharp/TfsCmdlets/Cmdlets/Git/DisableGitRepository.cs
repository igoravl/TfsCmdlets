using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Disables one or more Git repositories.
    /// </summary>
    /// <remarks>
    /// Disables access to the repository. When a repository is disabled it cannot be 
    /// accessed (including clones, pulls, pushes, builds, pull requests etc) 
    /// but remains discoverable, with a warning message stating it is disabled.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(GitRepository))]
    partial class DisableGitRepository
    {
        /// <summary>
        /// Specifies the name or ID of a Git repository. Wildcards are supported. 
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; }
    }
}
