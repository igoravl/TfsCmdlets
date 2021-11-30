using System;
using System.Collections.Generic;
using System.Composition;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Cmdlets.Wiki;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.Wiki
{
    [CmdletController]
    internal class NewWikiController : ControllerBase<WikiV2>
    {
        public override IEnumerable<WikiV2> Invoke()
        {
            var tp = Data.GetProject();
            var isProjectWiki = parameters.Get<bool>(nameof(NewWiki.ProjectWiki));

            var createParams = new WikiCreateParametersV2()
            {
                Name = parameters.Get<string>(nameof(NewWiki.Wiki)),
                Type = isProjectWiki ? WikiType.ProjectWiki : WikiType.CodeWiki,
                ProjectId = tp.Id
            };

            var repoName = parameters.Get<string>(nameof(NewWiki.Repository));

            var repoInfo = parameters.Override(new
                {
                    Repository = repoName,
                    Project = tp
                });

            if (createParams.Type == WikiType.CodeWiki)
            {
                if(!Data.TestItem<GitRepository>(repoInfo))
                {
                    throw new ArgumentException($"The repository '{repoName}' does not exist. To create a new code-based Wiki, you need to provide the name of an existing Git repository.");
                }

                var repo = Data.GetItem<GitRepository>(repoInfo);

                createParams.RepositoryId = repo.Id;
                createParams.MappedPath = parameters.Get<string>(nameof(NewWiki.Path));
                createParams.Version = new GitVersionDescriptor() {
                    VersionType = GitVersionType.Branch,
                    Version = parameters.Get<string>(nameof(NewWiki.Branch)) ?? repo.DefaultBranch.Substring(repo.DefaultBranch.LastIndexOf('/') + 1),
                    VersionOptions = GitVersionOptions.None
                };
            }

            var client = Data.GetClient<WikiHttpClient>(parameters);

            yield return client.CreateWikiAsync(createParams)
                .GetResult("Error creating Wiki repository");
        }

        [ImportingConstructor]
        public NewWikiController(IPowerShellService powerShell, IDataManager data, ILogger logger)
            : base(powerShell, data, logger)
        {
        }
    }
}