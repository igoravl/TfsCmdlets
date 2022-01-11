using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Cmdlets.Git;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class RenameGitRepositoryController 
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var repoToRename = Data.GetItem<GitRepository>();
            var newName = Parameters.Get<string>(nameof(RenameGitRepository.NewName));

            if (!PowerShell.ShouldProcess(tp, $"Rename Git repository [{repoToRename.Name}] to '{newName}'"))
                yield break;

            var client = Data.GetClient<GitHttpClient>();

            yield return client.RenameRepositoryAsync(repoToRename, newName)
                .GetResult("Error renaming repository");
        }
   }
}