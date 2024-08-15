using System.Linq;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Models
{
    public class TypeProcessor : BaseTypeProcessor
    {
        public object DataType { get; set; }

        protected override void OnInitialize()
        {
            DataType = Type.GetAttributeConstructorValue<INamedTypeSymbol>("ModelAttribute"); ;
        }

        public override string GenerateCode()
        {
            return $@"namespace {Namespace}
{{
/*
InnerType: {DataType.GetType()}
*/
    public partial class {Type.Name}: ModelBase<{DataType}>
    {{
        public {Type.Name}({DataType} obj): base(obj) {{ }}
        public static implicit operator {Type}({DataType} obj) => new {Type}(obj);
        public static implicit operator {DataType}({Type} obj) => obj.InnerObject;
    }}
}}
";
        }
    }
}
