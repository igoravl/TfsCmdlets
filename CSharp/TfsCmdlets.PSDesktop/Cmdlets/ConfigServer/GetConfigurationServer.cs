using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    /// <summary>
    /// Gets information about a configuration server.
    /// </summary>
    [OutputType(typeof(Microsoft.TeamFoundation.Client.TfsConfigurationServer))]
    partial class GetConfigurationServer 
    {
        partial void DoProcessRecord()
        {
            WriteObject(this.GetServer());
        }
    }
}
