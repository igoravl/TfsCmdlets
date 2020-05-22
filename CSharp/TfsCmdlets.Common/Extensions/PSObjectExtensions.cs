using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace TfsCmdlets.Extensions
{
    internal static class PSObjectExtensions
    {
        internal static void AddNoteProperty(this PSObject obj, string name, object value)
        {
            obj.Properties.Add(new PSNoteProperty(name, value));
        }
    }
}
