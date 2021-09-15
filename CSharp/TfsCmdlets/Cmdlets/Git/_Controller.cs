//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Management.Automation;
//using Microsoft.TeamFoundation.Core.WebApi;
//using Microsoft.TeamFoundation.SourceControl.WebApi;
//using Microsoft.VisualStudio.Services.Search.WebApi.Contracts;
//using TfsCmdlets.Extensions;
//using TfsCmdlets.HttpClient;
//using TfsCmdlets.Models;
//using TfsCmdlets.Services;

//namespace TfsCmdlets.Cmdlets.Git
//{
//    [Exports(typeof(GitRepository))]
//    internal partial class GitRepositoryController : ProjectLevelController<GitRepository>
//    {
//        protected override IEnumerable<GitRepository> DoGetItems(ParameterDictionary parameters)
//        {
//            var tp = Project;
//            var repository = parameters.Get<object>("Repository");

//            while (true)
//                switch (repository)
//                {
//                    case null:
//                    case string s when string.IsNullOrEmpty(s):
//                    {
//                        repository = tp.Name;
//                        continue;
//                    }
//                    case GitRepository repo:
//                    {
//                        yield return repo;
//                        yield break;
//                    }
//                    case Guid guid:
//                    {
//                        yield return GetClient<GitHttpClient>()
//                            .GetRepositoryAsync(tp.Name, guid)
//                            .GetResult($"Error getting repository with ID {guid}");

//                        yield break;
//                    }
//                    case string s when s.IsGuid():
//                    {
//                        repository = new Guid(s);
//                        continue;
//                    }
//                    case string s when !s.IsWildcard():
//                    {
//                        GitRepository result;

//                        try
//                        {
//                            result = GetClient<GitHttpClient>()
//                                .GetRepositoryAsync(tp.Name, s)
//                                .GetResult($"Error getting repository '{s}'");
//                        }
//                        catch
//                        {
//                            // Workaround to retrieve disabled repositories
//                            result = GetClient<GitHttpClient>()
//                                .GetRepositoriesAsync(tp.Name)
//                                .GetResult($"Error getting repository(ies) '{s}'")
//                                .First(r => r.Name.Equals(s, StringComparison.OrdinalIgnoreCase));
//                        }

//                        yield return result;
//                        yield break;
//                    }
//                    case string s:
//                    {
//                        foreach (var repo in GetClient<GitHttpClient>()
//                            .GetRepositoriesAsync(tp.Name)
//                            .GetResult($"Error getting repository(ies) '{s}'")
//                            .Where(r => r.Name.IsLike(s)))
//                        {
//                            yield return repo;
//                        }

//                        yield break;
//                    }
//                    default:
//                    {
//                        throw new ArgumentException(nameof(GetGitRepository.Repository));
//                    }
//                }
//        }

//        public GitRepositoryController(Microsoft.TeamFoundation.Core.WebApi.TeamProject project,
//            TpcConnection collection,
//            ILogger logger,
//            IParameterManager parameterManager,
//            IPowerShellService powerShell)
//            : base(project, collection, logger, parameterManager, powerShell)
//        {
//        }

//        protected override IEnumerable<GitRepository> DoDisableItems(ParameterDictionary parameters)
//        {
//            var tp = Project;
//            var repos = GetItems(parameters);

//            var client = GetClient<GitExtendedHttpClient>();

//            foreach (var repo in repos)
//            {
//                if (!PowerShell.ShouldProcess($"Team project '{tp.Name}'", $"Disable Git repository '{repo.Name}'"))
//                    continue;

//                client.UpdateRepositoryEnabledStatus(tp.Name, repo.Id, false);

//                yield return repo;
//            }
//        }

//        protected override IEnumerable<GitRepository> DoEnableItems(ParameterDictionary parameters)
//        {
//            var tp = Project;
//            var repos = GetItems(parameters);
//            var client = GetClient<GitExtendedHttpClient>();

//            foreach (var repo in repos)
//            {
//                if (!PowerShell.ShouldProcess($"Team project '{tp.Name}'", $"Enable Git repository '{repo.Name}'"))
//                    continue;

//                client.UpdateRepositoryEnabledStatus(tp.Name, repo.Id, true);

//                yield return repo;
//            }
//        }

//        protected override GitRepository DoNewItem(ParameterDictionary parameters)
//        {
//            var tp = Project;
//            var repo = parameters.Get<string>(nameof(NewGitRepository.Repository));

//            if (!PowerShell.ShouldProcess(tp, $"Create Git repository '{repo}'")) return null;

//            var client = GetClient<GitHttpClient>();

//            var tpRef = new TeamProjectReference
//            {
//                Id = tp.Id,
//                Name = tp.Name
//            };

//            var repoToCreate = new GitRepository
//            {
//                Name = repo,
//                ProjectReference = tpRef
//            };

//            return client.CreateRepositoryAsync(repoToCreate, tp.Name)
//                .GetResult("Error creating Git repository");
//        }

//        protected override void DoRemoveItem(ParameterDictionary parameters)
//        {
//            var tp = Project;
//            var repos = GetItems(parameters);
//            var force = parameters.Get<bool>(nameof(RemoveGitRepository.Force));

//            var client = GetClient<GitHttpClient>();

//            foreach (var r in repos)
//            {
//                if (!PowerShell.ShouldProcess(tp, $"Delete Git repository '{r.Name}'")) continue;

//                if (!force &&
//                    !PowerShell.ShouldContinue($"Are you sure you want to delete Git repository '{r.Name}'?")) continue;

//                client.DeleteRepositoryAsync(r.Id).Wait();
//            }
//        }

//        protected override GitRepository DoRenameItem(ParameterDictionary parameters)
//        {
//            var tp = Project;
//            var repoToRename = GetItem(parameters);
//            var newName = parameters.Get<string>(nameof(RenameGitRepository.NewName));

//            if (!PowerShell.ShouldProcess(tp, $"Rename Git repository [{repoToRename.Name}] to '{newName}'"))
//                return null;

//            var client = GetClient<GitHttpClient>();

//            return client.RenameRepositoryAsync(repoToRename, newName)
//                .GetResult("Error renaming repository");
//        }
//    }
//}