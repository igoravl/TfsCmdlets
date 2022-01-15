using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;

namespace TfsCmdlets.Controllers.ServiceHook
{
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