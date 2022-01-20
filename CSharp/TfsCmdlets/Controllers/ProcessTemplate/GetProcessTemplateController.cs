using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.ProcessTemplate;

namespace TfsCmdlets.Controllers.ProcessTemplate
{
    /// <summary>
    /// Gets information from one or more process templates.
    /// </summary>
    [CmdletController(typeof(WebApiProcess))]
    partial class GetProcessTemplateController
    {
        protected override IEnumerable Run()
        {
            var client = Data.GetClient<ProcessHttpClient>();

            foreach (var pt in ProcessTemplate)
            {
                var process = pt switch
                {
                    string s0 when s0.IsGuid() => new Guid(s0),
                    _ => pt
                };

                switch (process)
                {
                    case WebApiProcess p:
                        {
                            yield return p;
                            break;
                        }
                    case Guid g:
                        {
                            yield return TaskExtensions.GetResult<Process>(client.GetProcessByIdAsync(g), $"Error getting process template '{process}'");

                            yield break;
                        }
                    case null when Default:
                    case { } when Default:
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
}