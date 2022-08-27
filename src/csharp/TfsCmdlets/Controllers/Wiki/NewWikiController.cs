using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Cmdlets.Wiki;

namespace TfsCmdlets.Controllers.Wiki
{
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