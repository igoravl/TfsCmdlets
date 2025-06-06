using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System.Linq;
using System.Collections.Generic;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Gets information from one or more Git repositories in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GitRepository), DefaultParameterSetName = "Get by ID or Name")]
    partial class GetGitRepository
    {
        /// <summary>
        /// Specifies the name or ID of a Git repository. Wildcards are supported. 
        /// When omitted, all Git repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by ID or Name")]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; } = "*";

        /// <summary>
        /// Returns the default repository in the given team project.
        /// The default repository is the one that is created when a team project is created, and has the same name as the team project.
        /// </summary>
        [Parameter(ParameterSetName = "Get default", Mandatory = true)]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// Returns details about the repository's parent (forked) repository, if it has one.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeParent { get; set; }

        /// <summary>
        /// Includes repositories in the recycle bin in the search results.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeRecycleBin { get; set; }
    }

    [CmdletController(typeof(GitRepository), Client=typeof(IGitHttpClient))]
    partial class GetGitRepositoryController
    {
        protected override IEnumerable Run()
        {
            // Get recycle bin repositories once if needed
            IList<GitDeletedRepository> recycleBinRepos = null;
            if (IncludeRecycleBin)
            {
                recycleBinRepos = Client
                    .GetRecycleBinRepositoriesAsync(Project.Name)
                    .GetResult("Error getting recycle bin repositories");
            }

            foreach (var input in Repository)
            {
                var repository = input switch
                {
                    string s when string.IsNullOrEmpty(s) => Project.Name,
                    string s when s.IsGuid() => new Guid(s),
                    null => Project.Name,
                    _ => input
                };

                switch (repository)
                {
                    case GitRepository repo:
                        {
                            yield return repo;
                            break;
                        }
                    case Guid guid:
                        {
                            yield return Client
                                .GetRepositoryAsync(Project.Name, guid, includeParent: IncludeParent)
                                .GetResult($"Error getting repository with ID {guid}");

                            // Also check recycle bin for this ID
                            if (IncludeRecycleBin)
                            {
                                var deletedRepo = recycleBinRepos?.FirstOrDefault(r => r.Id == guid);
                                if (deletedRepo != null)
                                {
                                    yield return deletedRepo;
                                }
                            }
                            break;
                        }
                    case { } when Default:
                        {
                            yield return Client
                                .GetRepositoryAsync(Project.Name, Project.Name, includeParent: IncludeParent)
                                .GetResult($"Error getting repository '{Project.Name}'");
                            break;
                        }
                    case string s when !s.IsWildcard():
                        {
                            GitRepository result = null;

                            try
                            {
                                result = Client
                                    .GetRepositoryAsync(Project.Name, s, includeParent: IncludeParent)
                                    .GetResult($"Error getting repository '{s}'");
                                yield return result;
                            }
                            catch
                            {
                                // Workaround to retrieve disabled repositories
                                var activeRepo = Client
                                    .GetRepositoriesAsync(Project.Name, includeLinks: true, includeHidden: true)
                                    .GetResult($"Error getting repository(ies) '{s}'")
                                    .FirstOrDefault(r => r.Name.Equals(s, StringComparison.OrdinalIgnoreCase));

                                if (activeRepo != null)
                                {
                                    if (IncludeParent)
                                    {
                                        result = Client
                                            .GetRepositoryAsync(Project.Name, activeRepo.Id, includeParent: true)
                                            .GetResult($"Error getting repository(ies) '{s}'");
                                        yield return result;
                                    }
                                    else
                                    {
                                        yield return activeRepo;
                                    }
                                }
                            }

                            // Also check recycle bin for this name
                            if (IncludeRecycleBin)
                            {
                                var deletedRepo = recycleBinRepos?.FirstOrDefault(r => r.Name.Equals(s, StringComparison.OrdinalIgnoreCase));
                                if (deletedRepo != null)
                                {
                                    yield return deletedRepo;
                                }
                            }
                            break;
                        }
                    case string s:
                        {
                            foreach (var repo in Client
                                .GetRepositoriesAsync(Project.Name, includeLinks: true)
                                .GetResult($"Error getting repository(ies) '{s}'")
                                .Where(r => r.Name.IsLike(s)))
                            {
                                if (IncludeParent)
                                {
                                    yield return Client
                                        .GetRepositoryAsync(Project.Name, repo.Id, includeParent: true)
                                        .GetResult($"Error getting repository(ies) '{s}'");
                                }
                                else
                                {
                                    yield return repo;
                                }
                            }

                            // Also check recycle bin for wildcard patterns
                            if (IncludeRecycleBin)
                            {
                                foreach (var deletedRepo in recycleBinRepos?.Where(r => r.Name.IsLike(s)) ?? Enumerable.Empty<GitDeletedRepository>())
                                {
                                    yield return deletedRepo;
                                }
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent repository '{repository}'"));
                            break;
                        }
                }
            }
        }
    }
}