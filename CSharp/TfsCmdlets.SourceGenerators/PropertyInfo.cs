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

        public string Visibility { get; set; }

        public bool HasGetAccessor { get; set; }

        public string GetAccessorVisibility { get; set; }

        public bool HasSetAccessor { get; set; }

        public string SetAccessorVisibility { get; set; }

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
            Visibility = prop.DeclaredAccessibility.ToString().ToLowerInvariant();
            HasGetAccessor = !prop.IsWriteOnly;
            HasSetAccessor = !prop.IsReadOnly;
            GetAccessorVisibility = string.Empty;
            SetAccessorVisibility = string.Empty;

            if (prop.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() is not PropertyDeclarationSyntax node) return;

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
            return string.IsNullOrEmpty(GeneratedCode)
                ? $$"""
                    {{Visibility}} {{Type}} {{Name}} { {{(HasGetAccessor ? "get; " : string.Empty)}}{{(HasSetAccessor ? "set; " : string.Empty)}}}
                    """
                : GeneratedCode;
        }
    }
}