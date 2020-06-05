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

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
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

            var (tpc, tp) = this.GetCollectionAndProject();
            var client = GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
            //        var reposToDelete = Get - TfsGitRepository - Name Repository - Project Project - Collection Collection
            //            }

            //        foreach (repo in reposToDelete)
            //        {
            //            if (ShouldProcess(repo.Name, $"Delete Git repository from Team Project {{tp}.Name}"))
            //            {
            //                client.DeleteRepositoryAsync(repo.Id).Wait()
            //                }
            //        }
            //    }
            //}

            //    */
        }
    }
}