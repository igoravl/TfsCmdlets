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
    public partial class GetProcessTemplate : CmdletBase
    {
        /// <summary>
        /// Specifies the name of the process template(s) to be returned. Wildcards are supported. 
        /// When omitted, all process templates in the given project collection are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by name")]
        [Alias("Name")]
        [SupportsWildcards()]
        public object ProcessTemplate { get; set; } = "*";

        /// <summary>
        /// Returns the default process template in the given orgnization / project collection.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get default process")]
        public SwitchParameter Default { get; set; }
    }

    // TODO

    //[Exports(typeof(WebApiProcess))]
    //internal partial class ProcessDataService : CollectionLevelController<WebApiProcess>
    //{
    //    protected override IEnumerable<WebApiProcess> DoGetItems()
    //    {
    //        var process = parameters.Get<object>(nameof(GetProcessTemplate.ProcessTemplate));
    //        var isDefault = parameters.Get<bool>(nameof(GetProcessTemplate.Default));

    //        var client = GetClient<ProcessHttpClient>();

    //        while (true) switch (process)
    //            {
    //                case WebApiProcess p:
    //                    {
    //                        yield return p;
    //                        yield break;
    //                    }
    //                case string s when s.IsGuid():
    //                    {
    //                        process = new Guid(s);
    //                        continue;
    //                    }
    //                case Guid g:
    //                    {
    //                        yield return client.GetProcessByIdAsync(g)
    //                            .GetResult($"Error getting process template '{process}'");

    //                        yield break;
    //                    }
    //                case object o when isDefault:
    //                    {
    //                        foreach (var proc in client.GetProcessesAsync()
    //                            .GetResult($"Error getting process templates")
    //                            .Where(p => p.IsDefault))
    //                        {
    //                            yield return proc;
    //                        }

    //                        yield break;
    //                    }
    //                case string s:
    //                    {
    //                        foreach (var proc in client.GetProcessesAsync()
    //                            .GetResult($"Error getting process template '{process}'")
    //                            .Where(p => p.Name.IsLike(s)))
    //                        {
    //                            yield return proc;
    //                        }

    //                        yield break;
    //                    }
    //                default:
    //                    {
    //                        throw new ArgumentException($"Invalid or non-existent process '{process}'");
    //                    }
    //            }
    //    }
    //}
}