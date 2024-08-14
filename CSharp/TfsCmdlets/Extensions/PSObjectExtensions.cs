using System.Management.Automation;

namespace TfsCmdlets.Extensions
{
    public static class PSObjectExtensions
    {
        public static void SetProperty(this PSObject obj, string name, object value)
        {
            if (obj.Properties[name] == null)
            {
                obj.Properties.Add(new PSNoteProperty(name, value));
            }
            else
            {
                obj.Properties[name].Value = new PSNoteProperty(name, value);
            }
        }
        
        public static PSPropertyInfo GetProperty(this PSObject obj, string name)
        {
            if (obj.Properties[name] == null)
            {
                SetProperty(obj, name, null);
            }

            return obj.Properties[name];
        }

    }
}
