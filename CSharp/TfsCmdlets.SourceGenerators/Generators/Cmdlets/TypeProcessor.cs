using System.Linq;
using System.Text;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    public class TypeProcessor : BaseTypeProcessor
    {
        protected override void OnInitialize() { }

        public override string GenerateCode()
        {
            //Logger.Log(" - Initializing CmdletInfo");

            var cmdlet = new CmdletInfo(Type, Logger);

            //Logger.Log($" - {cmdlet.Name} has the following properties: {string.Join(", ", cmdlet.GeneratedProperties.Values.Select(p => p.Name))}");

            var props = new StringBuilder();

            foreach(var prop in cmdlet.GeneratedProperties.Values) props.Append(prop.ToString());

            return $@"
namespace {Namespace}
{{
    {cmdlet.CmdletAttribute}{cmdlet.OutputTypeAttribute}
    public partial class {cmdlet.Name}: CmdletBase
    {{{props}
    }}
}}
";
        }
    }
}