using Microsoft.TeamFoundation.Policy.WebApi;

namespace TfsCmdlets.Controllers.Git.Policy
{
    [CmdletController(typeof(PolicyType))]
    partial class GetGitPolicyTypeController
    {
        protected override IEnumerable Run()
        {
            var policyType = Parameters.Get<object>("PolicyType");

            while (true) switch (policyType)
            {
                case PolicyType pt:
                    {
                        yield return pt;
                        yield break;
                    }
                case string s when s.IsGuid():
                    {
                        policyType = new Guid(s);
                        continue;
                    }
                case Guid g:
                    {
                        var tp = Data.GetProject();
                        var client = Data.GetClient<PolicyHttpClient>();
                        yield return client.GetPolicyTypeAsync(tp.Name, g)
                            .GetResult("Error retrieving policy types");
                        yield break;
                    }
                case string s:
                    {
                        var tp = Data.GetProject();
                        var client = Data.GetClient<PolicyHttpClient>();
                        foreach (var pt in client.GetPolicyTypesAsync(tp.Name)
                            .GetResult("Error retrieving policy types")
                            .Where(p => p.DisplayName.IsLike(s)))
                        {
                            yield return pt;
                        }

                        yield break;
                    }
                default:
                    {
                        throw new ArgumentException($"Invalid policy type '{policyType}'", nameof(policyType));
                    }
            }
        }
    }
}