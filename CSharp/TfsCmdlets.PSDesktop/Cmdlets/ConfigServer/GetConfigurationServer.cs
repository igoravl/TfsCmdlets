using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    [OutputType(typeof(Microsoft.TeamFoundation.Client.TfsConfigurationServer))]
    partial class GetConfigurationServer 
    {
        /// <inheritdoc/>
        protected override void DoProcessRecord()
        {
            WriteObject(this.GetServer());
        }
    }
}
