using System.Management.Automation;

namespace TfsCmdlets.Util
{
    public class PSJsonConverter
    {
        public static object Deserialize(string input, bool noAutoUnwrap)
        {
            string script;

            script = noAutoUnwrap ? 
                "ConvertFrom-Json -InputObject $args[0]" :
                "$o = ConvertFrom-Json -InputObject $args[0]; if ($o.Value) {return $o.Value} else {return $o}";

            var sb = ScriptBlock.Create(script);
            var jsonObject = sb.Invoke(input);

            return jsonObject;
        }
    }
}