using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

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
    }

    [CmdletController(typeof(GitRepository), Client=typeof(IGitHttpClient))]
    partial class GetGitRepositoryController
    {
        protected override IEnumerable Run()
        {
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
                            GitRepository result;

                            try
                            {
                                result = Client
                                    .GetRepositoryAsync(Project.Name, s, includeParent: IncludeParent)
                                    .GetResult($"Error getting repository '{s}'");
                            }
                            catch
                            {
                                // Workaround to retrieve disabled repositories
                                result = Client
                                    .GetRepositoriesAsync(Project.Name, includeLinks: true, includeHidden: true)
                                    .GetResult($"Error getting repository(ies) '{s}'")
                                    .First(r => r.Name.Equals(s, StringComparison.OrdinalIgnoreCase));

                                if (IncludeParent)
                                {
                                    result = Client
                                        .GetRepositoryAsync(Project.Name, result.Id, includeParent: true)
                                        .GetResult($"Error getting repository(ies) '{s}'");
                                }
                            }
                            yield return result;
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