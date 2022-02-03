using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    public class TypeProcessor : BaseTypeProcessor
    {
        protected override void OnInitialize()
        {
            var fields = Type.GetMembers().OfType<IFieldSymbol>().ToList();

            foreach (var field in fields)
            {
                var attr = field.GetAttributes().FirstOrDefault(a => a.AttributeClass.Name == "ParameterAttribute");

                if (attr == null) continue;

                Logger.ReportDiagnostic(DiagnosticDescriptors.ParameterMustBeProperty, Context, field);

            }
        }

        public override string GenerateCode()
        {
            //Logger.Log(" - Initializing CmdletInfo");

            var cmdlet = new CmdletInfo(Type, Logger);

            //Logger.Log($" - {cmdlet.Name} has the following properties: {string.Join(", ", cmdlet.GeneratedProperties.Values.Select(p => p.Name))}");

            var props = new StringBuilder();

            foreach (var prop in cmdlet.GeneratedProperties.Values) props.Append(prop.ToString());

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