using System;
using System.Management.Automation;
using System.Reflection;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets
{
    public abstract class BaseCmdlet: PSCmdlet
    {
        protected override void BeginProcessing()
        {
            CheckWindowsOnly();
            this.LogParameters();
        }

        private void CheckWindowsOnly()
        {
            if(EnvironmentUtil.PSEdition.Equals("Desktop") || GetType().GetCustomAttribute<WindowsOnlyAttribute>() == null)
            {
                return;
            }

            throw new NotSupportedException("This cmdlet requires Windows PowerShell. It will not work on PowerShell Core.");
        }
    }
}
