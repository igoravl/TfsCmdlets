using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Policy
{
    /// <summary>
    /// Gets one or more Git branch policies supported by the given team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(PolicyType))]
    partial class GetGitPolicyType
    {
        /// <summary>
        /// Specifies the display name or ID of the policy type. Wildcards are supported.
        /// When omitted, all policy types supported by the given team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object PolicyType { get; set; } = "*";
    }

    [CmdletController(typeof(PolicyType), Client=typeof(IPolicyHttpClient))]
    partial class GetGitPolicyTypeController
    {
        protected override IEnumerable Run()
        {
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
                            yield return Client.GetPolicyTypeAsync(Project.Name, g)
                                .GetResult("Error retrieving policy types");
                            break;
                        }
                    case string s:
                        {
                            foreach (var pt in Client.GetPolicyTypesAsync(Project.Name)
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