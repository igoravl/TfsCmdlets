using System.Management.Automation;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets one or more service hook consumers.
    /// </summary>
    /// <remarks>
    /// Service hook consumers are the services that can consume (receive) notifications triggered by 
    /// Azure DevOps. Examples of consumers available out-of-box with Azure DevOps are Microsoft Teams, 
    /// Slack, Trello ou the generic WebHook consumer. Use this cmdlet to list the available consumers and get 
    /// the ID of the desired one to be able to manage service hook subscriptions.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiConsumer))]
    partial class GetServiceHookConsumer
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
    }

    [CmdletController(typeof(WebApiConsumer), Client=typeof(ServiceHooksPublisherHttpClient))]
    partial class GetServiceHookConsumerController
    {
        protected override IEnumerable Run()
        {
            foreach (var consumer in Consumer)
            {
                switch (consumer)
                {
                    case WebApiConsumer c:
                        {
                            yield return c;
                            break;
                        }
                    case string s:
                        {
                            var result = Client.GetConsumersAsync()
                                .GetResult("Error getting service hook consumers");

                            foreach (var shc in result.Where(c => c.Name.IsLike(s) || c.Id.IsLike(s)))
                            {
                                yield return shc;
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent service hook consumer {consumer}"));
                            break;
                        }
                }
            }
        }
    }
}