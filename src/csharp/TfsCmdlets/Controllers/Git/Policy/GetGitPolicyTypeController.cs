using Microsoft.TeamFoundation.Policy.WebApi;

namespace TfsCmdlets.Controllers.Git.Policy
{
    [CmdletController(typeof(PolicyType))]
    partial class GetGitPolicyTypeController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<PolicyHttpClient>();

            foreach (var input in PolicyType)
            {
                var policyType = input switch
                {
                    string s when s.IsGuid() => new Guid(s),
                    _ => input
                };

                switch (policyType)
                {
                    case PolicyType pt:
                        {
                            yield return pt;
                            break;
                        }
                    case Guid g:
                        {
                            yield return client.GetPolicyTypeAsync(Project.Name, g)
                                .GetResult("Error retrieving policy types");
                            break;
                        }
                    case string s:
                        {
                            foreach (var pt in client.GetPolicyTypesAsync(Project.Name)
                                .GetResult("Error retrieving policy types")
                                .Where(p => p.DisplayName.IsLike(s)))
                            {
                                yield return pt;
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid policy type '{policyType}'", nameof(policyType)));
                            break;
                        }
                }
            }
        }
    }
}