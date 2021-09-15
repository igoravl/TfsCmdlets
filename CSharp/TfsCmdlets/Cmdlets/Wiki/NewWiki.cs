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
    [Cmdlet(VerbsCommon.New, "TfsWiki", SupportsShouldProcess = true)]
    [OutputType(typeof(WikiV2))]
    public class NewWiki : CmdletBase
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
        /// Creates a provisioned ("project") Wiki in the specified Team Project.
        /// </summary>
        [Parameter(ParameterSetName = "Provision Project Wiki", Mandatory = true)]
        public SwitchParameter ProjectWiki { get; set; }
    }

    // TODO

    //partial class WikiDataService
    //{
    //    protected override WikiV2 DoNewItem()
    //    {
    //        var (tpc, tp) = GetCollectionAndProject();
    //        var isProjectWiki = parameters.Get<bool>(nameof(NewWiki.ProjectWiki));

    //        var createParams = new WikiCreateParametersV2()
    //        {
    //            Name = parameters.Get<string>(nameof(NewWiki.Wiki)),
    //            Type = isProjectWiki ? WikiType.ProjectWiki : WikiType.CodeWiki,
    //            ProjectId = tp.Id
    //        };

    //        if(createParams.Type == WikiType.CodeWiki)
    //        {
    //            var repo = GetItem<GitRepository>(new {
    //                Repository = parameters.Get<object>(nameof(NewWiki.Repository)),
    //                Project = tp
    //            });

    //            createParams.RepositoryId = repo.Id;
    //        }

    //        var client = GetClient<WikiHttpClient>();

    //        return client.CreateWikiAsync(createParams)
    //            .GetResult("Error creating Wiki repository");
    //    }
    //}
}