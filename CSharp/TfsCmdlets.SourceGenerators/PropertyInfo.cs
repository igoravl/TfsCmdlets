using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace TfsCmdlets.SourceGenerators
{
    public record PropertyInfo
    {
        public string Name { get; }

        public string Type { get; }

        public string DefaultValue { get; set; }

        public bool IsHidden { get; set;  }

        public bool IsScope { get; set; }

        public string GeneratedCode { get; set; }

        public PropertyInfo(IPropertySymbol prop)
            : this(prop, string.Empty)
        {
        }

        public PropertyInfo(IPropertySymbol prop, string generatedCode)
            : this(prop.Name, prop.Type.ToString(), generatedCode)
        {
            var node = (PropertyDeclarationSyntax) prop.DeclaringSyntaxReferences.First().GetSyntax();

            var initializer = node.Initializer;
            DefaultValue = initializer?.Value.ToString();
        }

        public PropertyInfo(string name, string typeName, string generatedCode)
        {
            Name = name;
            Type = typeName.EndsWith("SwitchParameter")
                ? "bool"
                : typeName;
            GeneratedCode = generatedCode;
        }

        public PropertyInfo(string name, string typeName, bool isHidden, string generatedCode)
            : this(name, typeName, generatedCode)
        {
            IsHidden = isHidden;
        }

        public override string ToString()
        {
            return GeneratedCode;
        }
    }
}