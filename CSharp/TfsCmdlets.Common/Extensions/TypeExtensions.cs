using System.Management.Automation;

namespace TfsCmdlets.Extensions
{
    internal static class TypeHelper
    {
        internal static bool ItemIs<T>(this PSCmdlet cmdlet, object item)
        {
            if (!(item is T))
            {
                return false;
            }

            cmdlet.Log($"Input item is of type {typeof(T).Name}; returning input item immediately, without further processing.");
            
            return true;
        }

    }
}
