using System;

namespace TfsCmdlets.Util
{
    public static class ErrorUtil
    {
        public static void ThrowDesktopOnlyCmdlet()
        {
            throw new NotSupportedException("This cmdlet requires Windows PowerShell. It does not work on PowerShell 6+.");
        }

        public static void ThrowWindowsOnlyCmdlet()
        {
            throw new NotSupportedException("This cmdlet requires Windows. It does not work on other platforms such as Linux and MacOS.");
        }

        public static void ThrowIfNotFound(object data, string name, object searchCriteria)
        {
            if (data == null)
            {
                throw new ArgumentException($"Invalid or non-existent {name} '{searchCriteria}'", name);
            }
        }

        public static void ThrowIfNull(object parameter, string parameterName, string message = null)
        {
            if (parameter != null) return;

            if (message != null) throw new ArgumentNullException(parameterName, message);

            throw new ArgumentNullException(parameterName);
        }
    }
}