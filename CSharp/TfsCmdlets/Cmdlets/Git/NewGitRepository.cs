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
    }
}
