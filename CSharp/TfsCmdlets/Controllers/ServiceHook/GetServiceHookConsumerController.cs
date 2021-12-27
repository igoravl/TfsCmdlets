using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;

namespace TfsCmdlets.Controllers.ServiceHook
{
    [CmdletController(typeof(WebApiConsumer))]
    partial class GetServiceHookConsumerController
    {
        public override IEnumerable<WebApiConsumer> Invoke()
        {
            var consumer = Parameters.Get<object>("Consumer");

            while (true) switch (consumer)
                {
                    case WebApiConsumer c:
                        {
                            yield return c;
                            yield break;
                        }
                    case string s:
                        {
                            var client = Data.GetClient<ServiceHooksPublisherHttpClient>();
                            var result = client.GetConsumersAsync().GetResult("Error getting service hook consumers");

                            foreach (var shc in result.Where(c => c.Name.IsLike(s) || c.Id.IsLike(s)))
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