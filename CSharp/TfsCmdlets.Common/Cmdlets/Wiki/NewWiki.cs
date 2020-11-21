using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Creates a new Wiki repository in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsWiki", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WikiV2))]
    public class NewWiki : NewCmdletBase<WikiV2>
    {
        /// <summary>
        /// Specifies the name of the new repository
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Create Code Wiki")]
        [Alias("Name", "Id")]
        public string Wiki { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Create Code Wiki")]
        public object Repository { get; set; }

        [Parameter(ParameterSetName = "Provision Project Wiki")]
        public SwitchParameter ProjectWiki { get; set; }
    }

    partial class WikiDataService
    {
        protected override WikiV2 DoNewItem()
        {
            var (tpc, tp) = GetCollectionAndProject();
            var isProjectWiki = GetParameter<bool>(nameof(NewWiki.ProjectWiki));

            var createParams = new WikiCreateParametersV2()
            {
                Name = GetParameter<string>(nameof(NewWiki.Wiki)),
                Type = isProjectWiki ? WikiType.ProjectWiki : WikiType.CodeWiki,
                ProjectId = tp.Id
            };

            if(createParams.Type == WikiType.CodeWiki)
            {
                var repo = GetItem<GitRepository>(new {
                    Repository = GetParameter<object>(nameof(NewWiki.Repository)),
                    Project = tp
                });

                createParams.RepositoryId = repo.Id;
            }

            var client = GetClient<WikiHttpClient>();

            return client.CreateWikiAsync(createParams)
                .GetResult("Error creating Wiki repository");
        }
    }
}