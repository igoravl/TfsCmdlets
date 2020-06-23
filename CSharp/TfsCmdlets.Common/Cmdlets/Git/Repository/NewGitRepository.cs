using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    /// <summary>
    /// Creates a new Git repository in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsGitRepository", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(GitRepository))]
    public class NewGitRepository : CmdletBase
    {
        /// <summary>
        /// Specifies the name of the new repository
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string Repository { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
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
            if (!ShouldProcess(Repository, "Create Git repository")) return;

            var (tpc, tp) = this.GetCollectionAndProject();
            var client = GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

            var tpRef = new TeamProjectReference()
            {
                Id = tp.Id,
                Name = tp.Name
            };

            var repoToCreate = new GitRepository()
            {
                Name = Repository,
                ProjectReference = tpRef
            };

            var result = client.CreateRepositoryAsync(repoToCreate, tp.Name).GetResult("Error create Git repository");

            if (Passthru)
            {
                WriteObject(result);
            }
        }
    }
}