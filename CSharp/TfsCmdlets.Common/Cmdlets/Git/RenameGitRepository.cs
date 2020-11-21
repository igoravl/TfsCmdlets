using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Renames a Git repository in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsGitRepository", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(GitRepository))]
    public class RenameGitRepository : RenameCmdletBase<GitRepository>
    {
        /// <summary>
        /// Specifies the repository to be renamed. Value can be the name or ID of a Git repository, 
        /// as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git
        /// repository.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Repository { get; set; }
    }

    partial class GitRepositoryDataService
    {
        protected override GitRepository DoRenameItem()
        {
            var (_, tp) = GetCollectionAndProject();
            var repoToRename = GetItem<GitRepository>();
            var newName = GetParameter<string>(nameof(RenameGitRepository.NewName));

            if (!ShouldProcess(tp, $"Rename Git repository [{repoToRename.Name}] to '{newName}'")) return null;

            var client = GetClient<GitHttpClient>();

            return client.RenameRepositoryAsync(repoToRename, newName)
                .GetResult("Error renaming repository");
        }
    }
}