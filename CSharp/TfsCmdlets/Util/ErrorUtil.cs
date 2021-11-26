using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TfsCmdlets.Util
{
    internal static class ErrorUtil
    {
        internal static void ThrowDesktopOnlyCmdlet()
        {
#if NETCOREAPP3_1_OR_GREATER
            throw new NotSupportedException("This cmdlet requires Windows PowerShell. It does not work on PowerShell 6+.");
#endif
        }

        internal static void ThrowIfNotFound(object data, string name, object searchCriteria)
        {
            if (data == null)
            {
                throw new ArgumentException($"Invalid or non-existent {name} '{searchCriteria}'", name);
            }
        }

        internal static void ThrowIfNull(object parameter, string parameterName, string message = null)
        {
            if (parameter != null) return;

            if (message != null) throw new ArgumentNullException(parameterName, message);

            throw new ArgumentNullException(parameterName);
        }
    }
}