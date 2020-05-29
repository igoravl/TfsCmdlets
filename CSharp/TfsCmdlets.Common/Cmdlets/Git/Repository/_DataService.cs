using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.ServiceProvider;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    [Exports(typeof(GitRepository))]
    internal class GitRepositoryDataService : BaseDataService<GitRepository>
    {
        protected override string ItemName => "Git Repository";

        protected override IEnumerable<GitRepository> GetItems(object userState)
        {
            var (tpc, tp) = Cmdlet.GetCollectionAndProject();
            var repository = ItemFilter = Parameters.Get<object>("Repository");

            while (true)
            {
                switch (repository)
                {
                    case PSObject o:
                        {
                            repository = o.BaseObject;
                            continue;
                        }
                    case GitRepository repo:
                        {
                            yield return repo;
                            yield break;
                        }
                    case Guid guid:
                        {
                            var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
                            yield return client.GetRepositoryAsync(tp.Name, guid).GetResult($"Error getting repository with ID {guid}");
                            yield break;
                        }
                    case string s when s.IsGuid():
                        {
                            repository = new Guid(s);
                            continue;
                        }
                    case string s when !s.IsWildcard():
                        {
                            var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
                            yield return client.GetRepositoryAsync(tp.Name, s).GetResult($"Error getting repository '{s}'");
                            yield break;
                        }
                    case string s:
                        {
                            var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

                            foreach (var repo in client.GetRepositoriesAsync(tp.Name).GetResult($"Error getting repository(ies) '{s}'").Where(r => r.Name.IsLike(s)))
                            {
                                yield return repo;
                            }
                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException(nameof(Repository));
                        }
                }
            }
        }
    }
}