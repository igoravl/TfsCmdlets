using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Gets information from one or more process templates.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiProcess))]
    partial class GetProcessTemplate 
    {
        /// <summary>
        /// Specifies the name of the process template(s) to be returned. Wildcards are supported. 
        /// When omitted, all process templates in the given project collection are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by name")]
        [Alias("Name", "Process")]
        [SupportsWildcards()]
        public object ProcessTemplate { get; set; } = "*";

        /// <summary>
        /// Returns the default process template in the given orgnization / project collection.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get default process")]
        public SwitchParameter Default { get; set; }
    }

    [CmdletController(typeof(WebApiProcess), Client=typeof(IProcessHttpClient))]
    partial class GetProcessTemplateController
    {
        protected override IEnumerable Run()
        {
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
                            yield return TaskExtensions.GetResult<Process>(Client.GetProcessByIdAsync(g), $"Error getting process template '{process}'");

                            yield break;
                        }
                    case null when Default:
                    case { } when Default:
                        {
                            foreach (var proc in TaskExtensions.GetResult<List<Process>>(Client.GetProcessesAsync(), $"Error getting process templates")
                                .Where(p => p.IsDefault))
                            {
                                yield return proc;
                            }

                            yield break;
                        }
                    case string s:
                        {
                            foreach (var proc in TaskExtensions.GetResult<List<Process>>(Client.GetProcessesAsync(), $"Error getting process template '{process}'")
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