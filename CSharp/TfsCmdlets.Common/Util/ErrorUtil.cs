using System;
using System.Linq;
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