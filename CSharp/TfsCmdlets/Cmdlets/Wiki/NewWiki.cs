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

    [CmdletController(typeof(WikiV2))]
    partial class NewWikiController
    {
        protected override IEnumerable Run()
        {
            var isProjectWiki = Parameters.Get<bool>(nameof(NewWiki.ProjectWiki));

            var createParams = new WikiCreateParametersV2()
            {
                Name = Parameters.Get<string>(nameof(NewWiki.Wiki)),
                Type = isProjectWiki ? WikiType.ProjectWiki : WikiType.CodeWiki,
                ProjectId = Project.Id
            };

            if(createParams.Type == WikiType.CodeWiki)
            {
                var repo = Data.GetItem<GitRepository>(new {
                    Repository = Parameters.Get<object>(nameof(NewWiki.Repository)),
                    Project
                });

                createParams.RepositoryId = repo.Id;
            }

            var client = Data.GetClient<WikiHttpClient>();

            yield return TaskExtensions.GetResult<WikiV2>(client.CreateWikiAsync(createParams), "Error creating Wiki repository");
        }
    }
}