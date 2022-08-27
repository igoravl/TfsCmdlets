using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Creates a new Git repository in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GitRepository))]
    partial class NewGitRepository
    {
        /// <summary>
        /// Specifies the name of the new repository
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string Repository { get; set; }

        /// <summary>
        /// Forks the specified reposity. To fork a repository from another team project, 
        /// specify the repository name in the form "project/repository" or pass in the result of a 
        /// previous call to Get-TfsGitRepository that returns the source repository.
        /// </summary>
        [Parameter()]
        public object ForkFrom { get; set; }

        /// <summary>
        /// Forks the specified branch in the source repository. When omitted, forks all branches.
        /// </summary>
        [Parameter()]
        public string SourceBranch { get; set; }
    }
}
