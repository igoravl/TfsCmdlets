using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace TfsCmdlets.SourceGenerators.Generators.Models
{
    public class TypeProcessor : BaseTypeProcessor
    {
        public object InnerType { get; set; }

        protected override void OnInitialize()
        {
            var node = Type.BaseType.TypeArguments.FirstOrDefault();

            InnerType = node;

        }

        public override string GenerateCode()
        {
            return $@"namespace {Namespace}
{{
/*
InnerType: {InnerType.GetType()}
*/
    public partial class {Type.Name}
    {{
        public static implicit operator {Type}({InnerType} obj) => new {Type}(obj);
        public static implicit operator {InnerType}({Type} obj) => obj.InnerObject;
    }}
}}
";
        }
    }
}
