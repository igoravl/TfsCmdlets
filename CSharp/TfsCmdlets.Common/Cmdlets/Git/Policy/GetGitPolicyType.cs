using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Policy
{
    /// <summary>
    /// Gets one or more Git branch policies supported by the given team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsGitPolicyType")]
    [OutputType(typeof(PolicyType))]
    public class GetGitPolicyType: CmdletBase
    {
        /// <summary>
        /// Specifies the display name or ID of the policy type. Wildcards are supported.
        /// When omitted, all policy types supported by the given team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object PolicyType { get; set; } = "*";

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

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteItems<PolicyType>();
        }
    }

    [Exports(typeof(PolicyType))]
    internal class PolicyTypeDataServiceImpl : BaseDataService<PolicyType>
    {
        protected override IEnumerable<PolicyType> DoGetItems()
        {
            var policyType = GetParameter<object>("PolicyType");

            while (true)
            {
                switch (policyType)
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
                            var (tpc, tp) = GetCollectionAndProject();
                            var client = GetClient<PolicyHttpClient>();
                            yield return client.GetPolicyTypeAsync(tp.Name, g)
                                .GetResult("Error retrieving policy types");

                            yield break;
                        }
                    case string s:
                        {
                            var (tpc, tp) = this.GetCollectionAndProject();
                            var client = GetClient<PolicyHttpClient>();
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
}