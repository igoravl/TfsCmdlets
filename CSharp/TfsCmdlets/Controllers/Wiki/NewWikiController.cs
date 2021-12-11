using System.Collections.Generic;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Cmdlets.Wiki;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.Wiki
{
    [CmdletController(typeof(WikiV2))]
    partial class NewWikiController
    {
        public override IEnumerable<WikiV2> Invoke()
        {
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();
            var isProjectWiki = Parameters.Get<bool>(nameof(NewWiki.ProjectWiki));

            var createParams = new WikiCreateParametersV2()
            {
                Name = Parameters.Get<string>(nameof(NewWiki.Wiki)),
                Type = isProjectWiki ? WikiType.ProjectWiki : WikiType.CodeWiki,
                ProjectId = tp.Id
            };

            if(createParams.Type == WikiType.CodeWiki)
            {
                var repo = Data.GetItem<GitRepository>(new {
                    Repository = Parameters.Get<object>(nameof(NewWiki.Repository)),
                    Project = tp
                });

                createParams.RepositoryId = repo.Id;
            }

            var client = Data.GetClient<WikiHttpClient>();

            yield return TaskExtensions.GetResult<WikiV2>(client.CreateWikiAsync(createParams), "Error creating Wiki repository");
        }
    }
}