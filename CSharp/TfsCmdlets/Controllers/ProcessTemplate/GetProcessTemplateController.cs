using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.ProcessTemplate;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;

namespace TfsCmdlets.Controllers.ProcessTemplate
{
    /// <summary>
    /// Gets information from one or more process templates.
    /// </summary>
    [CmdletController(typeof(WebApiProcess))]
    partial class GetProcessTemplateController
    {
       public override IEnumerable<WebApiProcess> Invoke()
       {
           var process = Parameters.Get<object>(nameof(GetProcessTemplate.ProcessTemplate));
           var isDefault = Parameters.Get<bool>(nameof(GetProcessTemplate.Default));

           var client = Data.GetClient<ProcessHttpClient>();

           while (true) switch (process)
               {
                   case WebApiProcess p:
                       {
                           yield return p;
                           yield break;
                       }
                   case string s when s.IsGuid():
                       {
                           process = new Guid(s);
                           continue;
                       }
                   case Guid g:
                       {
                           yield return TaskExtensions.GetResult<Process>(client.GetProcessByIdAsync(g), $"Error getting process template '{process}'");

                           yield break;
                       }
                   case object o when isDefault:
                       {
                           foreach (var proc in TaskExtensions.GetResult<List<Process>>(client.GetProcessesAsync(), $"Error getting process templates")
                               .Where(p => p.IsDefault))
                           {
                               yield return proc;
                           }

                           yield break;
                       }
                   case string s:
                       {
                           foreach (var proc in TaskExtensions.GetResult<List<Process>>(client.GetProcessesAsync(), $"Error getting process template '{process}'")
                               .Where(p => p.Name.IsLike(s)))
                           {
                               yield return proc;
                           }

                           yield break;
                       }
                   default:
                       {
                           throw new ArgumentException($"Invalid or non-existent process '{process}'");
                       }
               }
       }
    }
}