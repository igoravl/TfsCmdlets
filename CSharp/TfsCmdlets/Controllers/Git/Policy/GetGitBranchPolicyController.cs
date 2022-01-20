using Microsoft.TeamFoundation.Policy.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git.Policy
{
    [CmdletController(typeof(PolicyConfiguration))]
    partial class GetGitBranchPolicyController 
    {
        protected override IEnumerable Run()
        {
            var b = GetItem<GitBranchStats>();
            var url = new Uri(b.Commit.Url);
            var repoId = url.Segments[url.Segments.Length - 3].TrimEnd('/');
            var projectId = url.Segments[url.Segments.Length - 7].TrimEnd('/');
            var repo = GetItem<GitRepository>(new{Repository = repoId, Project = projectId});
            var client = GetClient<GitHttpClient>();

            var branch = $"refs/heads/{b.Name}";

            Logger.Log($"Getting branch policies in repository '{repo.Name}'");

            foreach (var policyType in PolicyType)
            {
                Guid policyTypeId = Guid.Empty;
                string pattern = null;

                switch (policyType)
                {
                    case PolicyType pt:
                        {
                            policyTypeId = pt.Id;
                            break;
                        }
                    case string s when s.IsGuid():
                        {
                            policyTypeId = new Guid(s);
                            break;
                        }
                    case string s:
                        {
                            pattern = s;
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid policy type '{policyType}'"));
                            break;
                        }
                }

                bool getById = (policyTypeId != Guid.Empty);

                var policies = (getById ?
                        client.GetPolicyConfigurationsAsync(repo.ProjectReference.Name, repo.Id, branch, policyTypeId): 
                        client.GetPolicyConfigurationsAsync(repo.ProjectReference.Name, repo.Id, branch))
                    .GetResult($"Error getting policy definitions from branch {branch} in repository {repo.Name}")
                    .PolicyConfigurations;

                foreach (var pol in policies) 
                {
                    if(!getById && !pol.Type.DisplayName.IsLike(pattern)) continue;

                    yield return pol;
                }
            }
        }
    }
}