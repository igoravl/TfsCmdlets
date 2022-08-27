using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;

namespace TfsCmdlets.Controllers.ServiceHook
{
    [CmdletController(typeof(WebApiConsumer))]
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
                            var client = GetClient<ServiceHooksPublisherHttpClient>();
                            var result = client.GetConsumersAsync()
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