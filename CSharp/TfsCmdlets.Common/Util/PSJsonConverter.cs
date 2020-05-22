using System.Management.Automation;

namespace TfsCmdlets.Util
{
    internal class PSJsonConverter
    {
        internal static object Deserialize(string input)
        {
            var sb = ScriptBlock.Create("ConvertFrom-Json -InputObject $args[0]");
            var jsonObject = sb.Invoke(input);

            return jsonObject;
        }
    }
}