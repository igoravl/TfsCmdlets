using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets
{
    public abstract class BaseCmdlet: PSCmdlet
    {
        protected override void BeginProcessing()
        {
            this.LogParameters();
        }
    }
}
