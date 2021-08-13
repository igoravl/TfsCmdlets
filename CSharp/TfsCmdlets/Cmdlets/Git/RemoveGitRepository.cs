using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Deletes one or more Git repositories from a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsGitRepository", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveGitRepository : RemoveCmdletBase<GitRepository>
    {
        /// <summary>
        /// Specifies the repository to be deleted. Value can be the name or ID of a Git repository, 
        /// as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git
        /// repository.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    partial class GitRepositoryDataService
    {
        protected override void DoRemoveItem()
        {
            var (_, tp) = GetCollectionAndProject();
            var client = GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

            var repos = GetItems<GitRepository>();
            var force = GetParameter<bool>(nameof(RemoveGitRepository.Force));

            foreach (var r in repos)
            {
                if (!ShouldProcess(tp, $"Delete Git repository [{r.Name}]")) continue;
                if(!force && !ShouldContinue($"Are you sure you want to delete Git repository '{r.Name}'?")) continue;

                client.DeleteRepositoryAsync(r.Id).Wait();
            }
        }
    }
}