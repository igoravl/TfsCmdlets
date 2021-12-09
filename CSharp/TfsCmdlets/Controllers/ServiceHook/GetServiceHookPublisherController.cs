using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Cmdlets.ServiceHook;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;

namespace TfsCmdlets.Controllers.ServiceHook
{
    [CmdletController(typeof(WebApiPublisher))]
    partial class GetServiceHookPublisherController
    {
       public override IEnumerable<WebApiPublisher> Invoke()
       {
           var client = Data.GetClient<ServiceHooksPublisherHttpClient>();
           var publisher = Parameters.Get<object>(nameof(GetServiceHookPublisher.Publisher));

           while (true) switch (publisher)
               {
                   case WebApiPublisher p:
                       {
                           yield return p;
                           yield break;
                       }
                   case string s:
                       {
                           var result = client.GetPublishersAsync()
                               .GetResult("Error getting service hook publishers")
                               .Where(p => p.Name.IsLike(s) || p.Id.IsLike(s));

                           foreach (var r in result) yield return r;
                           yield break;
                       }
                   default:
                       {
                           throw new ArgumentException($"Invalid or non-existent publisher '{publisher}'");
                       }
               }
       }
    }
}