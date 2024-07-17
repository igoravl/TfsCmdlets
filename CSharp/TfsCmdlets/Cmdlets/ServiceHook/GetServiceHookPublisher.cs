using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets one or more service hook publishers.
    /// </summary>
    /// <remarks>
    /// Service hook publishers are the components inside of Azure DevOps that can publish (send) notifications triggered by 
    /// event such as "work item changed" or "build queued". Use this cmdlet to list the available publishers and get 
    /// the ID of the desired one to be able to manage service hook subscriptions.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiPublisher))]
    partial class GetServiceHookPublisher
    {
        /// <summary>
        /// Specifies the name or ID of the service hook publisher to return. Wildcards are supported. 
        /// When omitted, returns all service hook consumers currently supported the current by Azure DevOps organization / 
        /// TFS collection.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name", "Id")]
        public object Publisher { get; set; } = "*";
    }

    [CmdletController(typeof(WebApiPublisher))]
    partial class GetServiceHookPublisherController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<ServiceHooksPublisherHttpClient>();

            foreach (var publisher in Publisher)
            {
                switch (publisher)
                {
                    case WebApiPublisher p:
                        {
                            yield return p;
                            break;
                        }
                    case string s:
                        {
                            var result = client.GetPublishersAsync()
                                .GetResult("Error getting service hook publishers")
                                .Where(p => p.Name.IsLike(s) || p.Id.IsLike(s));

                            foreach (var r in result) yield return r;
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent publisher '{publisher}'"));
                            break;
                        }
                }
            }
        }
    }
}