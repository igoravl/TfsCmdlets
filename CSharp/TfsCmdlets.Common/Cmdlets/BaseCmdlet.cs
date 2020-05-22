using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
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
