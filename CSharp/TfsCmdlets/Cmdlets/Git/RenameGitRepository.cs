using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Renames a Git repository in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(GitRepository))]
    partial class RenameGitRepository
    {
        /// <summary>
        /// Specifies the repository to be renamed. Value can be the name or ID of a Git repository, 
        /// as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git
        /// repository.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Repository { get; set; }
    }

    [CmdletController(typeof(GitRepository))]
    partial class RenameGitRepositoryController 
    {
        protected override IEnumerable Run()
        {
            var repoToRename = Items.FirstOrDefault();

            if(repoToRename == null)
            {
                Logger.LogError(new ArgumentException($"Invalid or non-existent repository '{Repository}'"));
                yield break;
            }

            if (!PowerShell.ShouldProcess(Project, $"Rename repository '{repoToRename.Name}' to '{NewName}'"))
                yield break;

            var client = GetClient<GitHttpClient>();

            yield return client.RenameRepositoryAsync(repoToRename, NewName)
                .GetResult("Error renaming repository");
        }
   }
}