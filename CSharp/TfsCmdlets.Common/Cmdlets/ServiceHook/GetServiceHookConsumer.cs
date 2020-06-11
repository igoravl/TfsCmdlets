using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets one or more service hook consumers.
    /// </summary>
    /// <remarks>
    /// Service hook consumers are the services that can consume (receive) notifications triggered by 
    /// Azure DevOps.false Examples of consumers available out-of-box with Azure DevOps are Microsoft Teams, 
    /// Slack, Trello ou a generic WebHook consumer. Use this cmdlet to list the available consumers and get 
    /// the ID of the desired one to be able to manage service hook subscriptions.
    /// </remarks>
    [Cmdlet(VerbsCommon.Get, "TfsServiceHookConsumer")]
    [OutputType(typeof(Consumer))]
    public class GetServiceHookConsumer : BaseCmdlet<Consumer>
    {
        /// <summary>
        /// Specifies the name or ID of the service hook consumer to return. Wildcards are supported. 
        /// When omitted, all service hook consumers registered in the given project collection/organization 
        /// are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name", "Id")]
        public string Consumer { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        /// <value></value>
        [Parameter()]
        public object Collection { get; set; }
    }

    [Exports(typeof(Consumer))]
    internal class ServiceHookConsumerDataService : BaseDataService<Consumer>
    {
        protected override IEnumerable<Consumer> DoGetItems()
        {
            var consumer = GetParameter<object>("Consumer");

            while (true) switch (consumer)
                {
                    case PSObject pso:
                        {
                            consumer = pso.BaseObject;
                            continue;
                        }
                    case Consumer c:
                        {
                            yield return c;
                            yield break;
                        }
                    case string s:
                        {
                            var client = GetClient<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient>();
                            var result = client.GetConsumersAsync().GetResult("Error getting service hook consumers");

                            foreach(var shc in result.Where(c=>c.Name.IsLike(s) || c.Id.IsLike(s)))
                            {
                                yield return shc;
                            }
                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent service hook consumer {consumer}");
                        }
                }
        }
    }
}