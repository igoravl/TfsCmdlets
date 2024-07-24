using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Enables one or more Git repositories.
    /// </summary>
    /// <remarks>
    /// This cmdlets re-enables access to a repository. When a repository is 
    /// disabled it cannot be accessed (including clones, pulls, pushes, builds, 
    /// pull requests etc) but remains discoverable, with a warning message 
    /// stating it is disabled.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(GitRepository))]
    partial class EnableGitRepository 
    {
        /// <summary>
        /// Specifies the name or ID of a Git repository. Wildcards are supported. 
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; }
    }

    [CmdletController(typeof(GitRepository))]
    partial class EnableGitRepositoryController 
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<GitExtendedHttpClient>();

            foreach (var repo in Items)
            {
                if (!PowerShell.ShouldProcess(Project, $"Disable Git repository '{repo.Name}'")) continue;

                client.UpdateRepositoryEnabledStatus(Project.Name, repo.Id, true);

                yield return GetItem<GitRepository>();
            }
        }
    }
}