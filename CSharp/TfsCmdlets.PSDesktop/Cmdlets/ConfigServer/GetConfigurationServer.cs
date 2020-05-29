using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    partial class GetConfigurationServer 
    {
        partial void DoProcessRecord()
        {
            WriteObject(this.GetServer());
        }
    }
}
