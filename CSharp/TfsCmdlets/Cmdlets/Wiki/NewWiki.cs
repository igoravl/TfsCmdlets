using System.Management.Automation;
using Microsoft.TeamFoundation.Wiki.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Creates a new Wiki repository in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WikiV2))]
    partial class NewWiki
    {
        /// <summary>
        /// Specifies the name of the new Wiki
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Create Code Wiki")]
        [Alias("Name", "Id")]
        public string Wiki { get; set; }

        /// <summary>
        /// Specifies the name or ID of the Git repository to publish as a Wiki
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, ParameterSetName = "Create Code Wiki")]
        public object Repository { get; set; }

        /// <summary>
        /// Specifies the name or ID of the source branch to publish as a Wiki. When ommited, the default branch is used.
        /// </summary>
        [Parameter(ParameterSetName = "Create Code Wiki")]
        public string Branch { get; set; }

        /// <summary>
        /// Specifies the path to the folder in the repository to publish as a Wiki. When ommited, defaults to the root folder.
        /// </summary>
        [Parameter(ParameterSetName = "Create Code Wiki")]
        public string Path { get; set; } = "/";

        /// <summary>
        /// Creates a provisioned ("project") Wiki in the specified Team Project.
        /// </summary>
        [Parameter(ParameterSetName = "Provision Project Wiki", Mandatory = true)]
        public SwitchParameter ProjectWiki { get; set; }
    }

    [CmdletController(typeof(WikiV2), Client=typeof(IWikiHttpClient))]
    partial class NewWikiController
    {
        protected override IEnumerable Run()
        {
            var createParams = new WikiCreateParametersV2()
            {
                Name = Wiki,
                Type = ProjectWiki ? WikiType.ProjectWiki : WikiType.CodeWiki,
                ProjectId = Project.Id
            };

            var action = "Create project wiki";

            if (createParams.Type == WikiType.CodeWiki)
            {
                var repo = Data.GetItem<GitRepository>(new
                {
                    Repository,
                    Project
                });

                var branch = Has_Branch && !string.IsNullOrWhiteSpace(Branch) ? Branch : repo.DefaultBranch;

                if (string.IsNullOrWhiteSpace(branch))
                {
                    throw new ArgumentException($"Repository '{repo.Name}' has no default branch. Specify a source branch with the -Branch parameter.", nameof(NewWiki.Branch));
                }

                const string branchPrefix = "refs/heads/";

                if (branch.StartsWith(branchPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    branch = branch.Substring(branchPrefix.Length);
                }

                createParams.RepositoryId = repo.Id;
                createParams.MappedPath = string.IsNullOrWhiteSpace(Path) ? "/" : Path;
                createParams.Version = new GitVersionDescriptor
                {
                    Version = branch,
                    VersionType = GitVersionType.Branch
                };

                action = $"Create code wiki '{Wiki}' from branch '{branch}' of repository '{repo.Name}'";
            }

            if (!PowerShell.ShouldProcess(Project, action)) yield break;

            yield return Client.CreateWikiAsync(createParams).GetResult("Error creating Wiki repository");
        }
    }
}
