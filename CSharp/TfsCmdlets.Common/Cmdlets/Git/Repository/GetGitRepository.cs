using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Git.Repository
{
    /// <summary>
    /// Gets information from one or more Git repositories in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "GitRepository")]
    [OutputType(typeof(GitRepository))]
    public class GetGitRepository : BaseCmdlet<GitRepository>
    {
        /// <summary>
        /// Specifies the name or ID (a GUID) of a Git repository. Wildcards are supported. 
        /// When omitted, all Git repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }
    }

    [Exports(typeof(GitRepository))]
    internal partial class GitRepositoryDataService : BaseDataService<GitRepository>
    {
        protected override IEnumerable<GitRepository> DoGetItems(object userState)
        {
            var (tpc, tp) = GetCollectionAndProject();
            var repository = Parameters.Get<object>("Repository");

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