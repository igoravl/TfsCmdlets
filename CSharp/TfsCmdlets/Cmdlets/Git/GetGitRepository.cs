using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Gets information from one or more Git repositories in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GitRepository))]
    partial class GetGitRepository
    {
        /// <summary>
        /// Specifies the name or ID of a Git repository. Wildcards are supported. 
        /// When omitted, all Git repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; } = "*";
    }
}