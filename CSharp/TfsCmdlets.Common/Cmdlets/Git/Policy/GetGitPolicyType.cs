using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Policy
{
    /// <summary>
    /// Gets one or more Git branch policies supported by the given team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "GitPolicyType")]
    [OutputType(typeof(PolicyType))]
    public class GetGitPolicyType : BaseCmdlet
    {
        /// <summary>
        /// Specifies the display name or ID of the policy type. Wildcards are supported.
        /// When omitted, all policy types supported by the given team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object PolicyType = "*";

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
            object result = null;

            while (result == null)
            {
                switch (PolicyType)
                {
                    case PSObject pso:
                        {
                            PolicyType = pso.BaseObject;
                            continue;
                        }
                    case PolicyType pt:
                        {
                            WriteObject(pt);
                            return;
                        }
                    case string s when s.IsGuid():
                    {
                        PolicyType = new Guid(s);
                        continue;
                    }
                    case Guid g:
                    {
                        var (tpc, tp) = this.GetCollectionAndProject();
                        var client = tpc.GetClient<PolicyHttpClient>();
                        result = client.GetPolicyTypeAsync(tp.Name, g)
                            .GetResult("Error retrieving policy types");

                        break;
                    }
                    case string s:
                    {
                        var (tpc, tp) = this.GetCollectionAndProject();
                        var client = tpc.GetClient<PolicyHttpClient>();
                        result = client.GetPolicyTypesAsync(tp.Name)
                            .GetResult("Error retrieving policy types")
                            .Where(p => p.DisplayName.IsLike(s));

                        break;
                    }
                    default:
                    {
                        throw new ArgumentException($"Invalid policy type {PolicyType}", nameof(PolicyType));
                    }
                }
            }

            WriteObject(result, true);
        }
    }
}