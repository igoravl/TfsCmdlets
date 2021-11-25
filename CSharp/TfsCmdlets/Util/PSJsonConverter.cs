using System.Management.Automation;

namespace TfsCmdlets.Util
{
    internal class PSJsonConverter
    {
        internal static object Deserialize(string input)
        {
            var sb = ScriptBlock.Create("$o = ConvertFrom-Json -InputObject $args[0]; if ($o.Value) {return $o.Value} else {return $o}");
            var jsonObject = sb.Invoke(input);

            return jsonObject;
        }
    }
}