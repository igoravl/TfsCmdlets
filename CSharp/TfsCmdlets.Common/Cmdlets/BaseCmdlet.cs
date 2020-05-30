using System;
using System.Management.Automation;
using System.Reflection;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Abstract class from which and TfsCmdlets cmdlets derive
    /// </summary>
    public abstract class BaseCmdlet: PSCmdlet
    {
        /// <summary>
        /// Performs initialization of the command execution, logs the supplied parameters and check whether the current 
        /// cmdlet is tagged as "Windows-only". If so, throws an exception
        /// </summary>
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
