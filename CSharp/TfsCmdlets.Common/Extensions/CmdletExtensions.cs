using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace TfsCmdlets.Extensions
{
    internal static class CmdletExtensions
    {
        internal static object InvokeScript(this PSCmdlet cmdlet, string script, params object[] arguments)
        {
            return cmdlet.InvokeCommand.InvokeScript(script, arguments);
        }
    }
}