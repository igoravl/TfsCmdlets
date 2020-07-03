using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Gets information from one or more process templates.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsProcessTemplate")]
    [OutputType(typeof(WebApiProcess))]
    public partial class GetProcessTemplate : GetCmdletBase<WebApiProcess>
    {
        /// <summary>
        /// Specifies the name of the process template(s) to be returned. Wildcards are supported. 
        /// When omitted, all process templates in the given project collection are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object ProcessTemplate { get; set; } = "*";
    }

    [Exports(typeof(WebApiProcess))]
    internal partial class ProcessDataService : BaseDataService<WebApiProcess>
    {
        protected override IEnumerable<WebApiProcess> DoGetItems()
        {
            var process = GetParameter<object>(nameof(GetProcessTemplate.ProcessTemplate));
            var client = GetClient<ProcessHttpClient>();

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
                            yield return client.GetProcessByIdAsync(g)
                                .GetResult($"Error getting process template '{process}'");

                            yield break;
                        }
                    case string s:
                        {
                            foreach (var proc in client.GetProcessesAsync()
                                .GetResult($"Error getting process template '{process}'")
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