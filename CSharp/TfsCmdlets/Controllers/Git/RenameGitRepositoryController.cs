using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Cmdlets.Git;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class RenameGitRepositoryController 
    {
        public override IEnumerable<GitRepository> Invoke()
        {
            var tp = Data.GetProject();
            var repoToRename = GetItem();
            var newName = Parameters.Get<string>(nameof(RenameGitRepository.NewName));

            if (!PowerShell.ShouldProcess(tp, $"Rename Git repository [{repoToRename.Name}] to '{newName}'"))
                yield break;

            var client = Data.GetClient<GitHttpClient>();

            yield return client.RenameRepositoryAsync(repoToRename, newName)
                .GetResult("Error renaming repository");
        }
   }
}