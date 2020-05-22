using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsCmdlets.Util
{
    internal static class ErrorUtil
    {
        internal static void ThrowDesktopOnlyCmdlet()
        {
            throw new NotSupportedException("This cmdlet is not supported on PowerShell Core");
        }
    }
}
