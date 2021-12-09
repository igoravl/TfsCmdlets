using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Enables one or more Git repositories.
    /// </summary>
    /// <remarks>
    /// This cmdlets re-enables access to a repository. When a repository is 
    /// disabled it cannot be accessed (including clones, pulls, pushes, builds, 
    /// pull requests etc) but remains discoverable, with a warning message 
    /// stating it is disabled.
    /// </remarks>
    [Cmdlet(VerbsLifecycle.Enable, "TfsGitRepository", SupportsShouldProcess = true)]
    [OutputType(typeof(GitRepository))]
    [TfsCmdlet(CmdletScope.Project)]
    partial class EnableGitRepository 
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