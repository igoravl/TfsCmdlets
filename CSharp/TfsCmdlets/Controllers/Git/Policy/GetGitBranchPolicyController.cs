using Microsoft.TeamFoundation.Policy.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git.Policy
{
    [CmdletController(typeof(PolicyConfiguration))]
    partial class GetGitBranchPolicyController 
    {
        public override IEnumerable<PolicyConfiguration> Invoke()
        {
            var repo = Data.GetItem<GitRepository>();
            var branch = $"refs/heads/{Data.GetItem<GitBranchStats>().Name}";
            var policyType = Parameters.Get<object>("PolicyType");

            while (true) switch (policyType)
            {
                case PolicyType pt:
                    {
                        policyType = pt.Id;
                        continue;
                    }
                case string s when s.IsGuid():
                    {
                        policyType = new Guid(s);
                        continue;
                    }
                case Guid g:
                    {
                        foreach (var pol in Data.GetClient<GitHttpClient>()
                            .GetPolicyConfigurationsAsync(repo.ProjectReference.Name, repo.Id, branch, g)
                            .GetResult($"Error getting policy definitions from branch {branch} in repository {repo.Name}")
                            .PolicyConfigurations)
                        {
                            yield return pol;
                        }

                        yield break;
                    }
                case string s:
                    {
                        foreach (var pol in Data.GetClient<GitHttpClient>()
                            .GetPolicyConfigurationsAsync(repo.ProjectReference.Name, repo.Id, branch)
                            .GetResult($"Error getting policy definitions from branch {branch} in repository {repo.Name}")
                            .PolicyConfigurations
                            .Where(pc => pc.Type.DisplayName.IsLike(s)))
                        {
                            yield return pol;
                        }

                        yield break;
                    }
                default:
                    {
                        throw new ArgumentException($"Invalid policy type '{policyType}'");
                    }
            }
        }
    }
}