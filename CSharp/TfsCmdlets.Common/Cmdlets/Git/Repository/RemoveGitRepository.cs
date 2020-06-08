using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    /// <summary>
    /// Deletes one or more Git repositories from a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsGitRepository", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveGitRepository : BaseCmdlet
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
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()] public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()] public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            if (Repository is Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository repo)
            {
                Project = repo.ProjectReference.Name;
            }

            var repos = this.GetCollectionOf<GitRepository>();
            var client = GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

            foreach (var r in repos)
            {
                if (!ShouldProcess($"Team Project [{r.ProjectReference.Name}]", $"Delete Git repository [{r.Name}]")) {continue;}

                client.DeleteRepositoryAsync(r.Id).Wait();
            }
        }
    }
}