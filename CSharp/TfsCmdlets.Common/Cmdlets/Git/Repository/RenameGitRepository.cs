using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    /// <summary>
    /// Renames a Git repository in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsGitRepository", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(GitRepository))]
    public class RenameGitRepository : CmdletBase
    {
        /// <summary>
        /// Specifies the repository to be renamed. Value can be the name or ID of a Git repository, 
        /// as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git
        /// repository.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Repository { get; set; }

        /// <summary>
        /// Specifies the new name of the item. Enter only a name, not a path and name.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string NewName { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            if (Repository is Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository repo)
            {
                Project = repo.ProjectReference.Name;
            }

            var repoToRename = GetItem<GitRepository>();
            var client = GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

            if (!ShouldProcess($"Team Project [{repoToRename.ProjectReference.Name}]",
                $"Rename Git repository [{repoToRename.Name}] to '{NewName}'")) { return; }

            var result = client.RenameRepositoryAsync(repoToRename, NewName).GetResult("Error renaming repository");

            if (Passthru)
            {
                WriteObject(result);
            }
        }
    }
}