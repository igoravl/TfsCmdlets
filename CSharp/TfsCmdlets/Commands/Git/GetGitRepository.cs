using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.Git
{
    [Command]
    internal class GetGitRepository : CommandBase<GitRepository>
    {
        public override IEnumerable<GitRepository> Invoke(ParameterDictionary parameters)
        {
            var tp = Data.GetProject();
            var repository = parameters.Get<object>(nameof(Cmdlets.Git.GetGitRepository.Repository));

            while (true) switch (repository)
            {
                case null:
                case string s when string.IsNullOrEmpty(s):
                {
                    repository = tp.Name;
                    continue;
                }
                case GitRepository repo:
                {
                    yield return repo;
                    yield break;
                }
                case Guid guid:
                {
                    yield return GetClient<GitHttpClient>()
                        .GetRepositoryAsync(tp.Name, guid)
                        .GetResult($"Error getting repository with ID {guid}");

                    yield break;
                }
                case string s when s.IsGuid():
                {
                    repository = new Guid(s);
                    continue;
                }
                case string s when !s.IsWildcard():
                {
                    GitRepository result;

                    try
                    {
                        result = GetClient<GitHttpClient>()
                            .GetRepositoryAsync(tp.Name, s)
                            .GetResult($"Error getting repository '{s}'");
                    }
                    catch
                    {
                        // Workaround to retrieve disabled repositories
                        result = GetClient<GitHttpClient>()
                            .GetRepositoriesAsync(tp.Name)
                            .GetResult($"Error getting repository(ies) '{s}'")
                            .First(r => r.Name.Equals(s, StringComparison.OrdinalIgnoreCase));
                    }

                    yield return result;
                    yield break;
                }
                case string s:
                {
                    foreach (var repo in GetClient<GitHttpClient>()
                        .GetRepositoriesAsync(tp.Name)
                        .GetResult($"Error getting repository(ies) '{s}'")
                        .Where(r => r.Name.IsLike(s)))
                    {
                        yield return repo;
                    }

                    yield break;
                }
                default:
                {
                    throw new ArgumentException("Repository");
                }
            }
        }

        [ImportingConstructor]
        public GetGitRepository(IPowerShellService powerShell, IConnectionManager connections, IDataManager data, ILogger logger)
            : base(powerShell, connections, data, logger)
        {
        }
    }
}
