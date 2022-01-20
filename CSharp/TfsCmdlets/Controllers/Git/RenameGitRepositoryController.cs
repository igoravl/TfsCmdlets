using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Cmdlets.Git;

namespace TfsCmdlets.Controllers.Git
{
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