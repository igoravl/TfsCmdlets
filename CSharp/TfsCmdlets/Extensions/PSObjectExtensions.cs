using System.Management.Automation;

namespace TfsCmdlets.Extensions
{
    internal static class PSObjectExtensions
    {
        internal static void AddNoteProperty(this PSObject obj, string name, object value)
        {
            obj.Properties.Add(new PSNoteProperty(name, value));
        }
        
        internal static PSPropertyInfo GetProperty(this PSObject obj, string name)
        {
            if (obj.Properties[name] == null)
            {
                AddNoteProperty(obj, name, null);
            }

            return obj.Properties[name];
        }

    }
}
