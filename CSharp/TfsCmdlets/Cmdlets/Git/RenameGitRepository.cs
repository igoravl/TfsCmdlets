using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Renames a Git repository in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsGitRepository", SupportsShouldProcess = true)]
    [OutputType(typeof(GitRepository))]
    public class RenameGitRepository : RenameCmdlet
    {
        /// <summary>
        /// Specifies the repository to be renamed. Value can be the name or ID of a Git repository, 
        /// as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git
        /// repository.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Repository { get; set; }
    }
}