using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Creates a new Git repository in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsGitRepository", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(GitRepository))]
    public class NewGitRepository : NewCmdletBase<GitRepository>
    {
        /// <summary>
        /// Specifies the name of the new repository
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string Repository { get; set; }

    }

    partial class GitRepositoryDataService
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override GitRepository DoNewItem()
        {
            var (_, tp) = GetCollectionAndProject();

            var repo = GetParameter<string>(nameof(NewGitRepository.Repository));

            if (!ShouldProcess(tp, $"Create Git repository '{repo}'")) return null;

            var client = GetClient<GitHttpClient>();

            var tpRef = new TeamProjectReference()
            {
                Id = tp.Id,
                Name = tp.Name
            };

            var repoToCreate = new GitRepository()
            {
                Name = repo,
                ProjectReference = tpRef
            };

            return client.CreateRepositoryAsync(repoToCreate, tp.Name)
                .GetResult("Error creating Git repository");
        }
    }
}