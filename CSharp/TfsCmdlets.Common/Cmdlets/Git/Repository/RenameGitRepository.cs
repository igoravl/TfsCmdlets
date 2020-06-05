using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    /// <summary>
    /// Renames a Git repository in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsGitRepository", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(GitRepository))]
    public class RenameGitRepository : BaseCmdlet
    {

        /// <summary>
        /// Specifies the repository to be renamed. Value can be the name or ID (a GUID) of a Git repository, 
        /// as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git
        /// repository.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public object Repository { get; set; }

        /// <summary>
        /// Specifies the new name of the item. Enter only a name, not a path and name.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string NewName { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
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

        /*
                /// <summary>
                /// Performs execution of the command
                /// </summary>
                protected override void ProcessRecord()
                    {
                        if (Repository is Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository)
                        {
                            Project = Repository.ProjectReference.Name
                        }

                        tp = this.GetProject();
                        #tpc = tp.Store.TeamProjectCollection

                        var client = GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

                        if (Repository is Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository)
                        {
                            reposToRename = @(Repository)
                        }
                        else
                        {
                            reposToRename = Get-TfsGitRepository -Name Repository -Project Project -Collection Collection
                        }

                        foreach(repo in reposToRename)
                        {
                            if (ShouldProcess(repo.Name, $"Rename Git repository in Team Project {{tp}.Name} to NewName"))
                            {
                                task = client.RenameRepositoryAsync(repo, NewName)
                                task.Wait()

                                if (Passthru)
                                {
                                    WriteObject(task.Result); return;
                                }
                            }
                        }
                    }
                }

                */
    }
}
