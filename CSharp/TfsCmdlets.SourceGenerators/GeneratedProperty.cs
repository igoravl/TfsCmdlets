using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace TfsCmdlets.SourceGenerators
{
    public class GeneratedProperty
    {
        public string Name { get; }
        public string Type { get; }
        public string DefaultValue { get; set; }
        public bool IsHidden { get; }
        public string GeneratedCode { get; set; }

        internal GeneratedProperty(IPropertySymbol prop, string generatedCode)
            : this(prop.Name, prop.Type.ToString(), generatedCode)
        {
            Logger.Log(prop.ToString());

            var node = (PropertyDeclarationSyntax) prop.DeclaringSyntaxReferences.First().GetSyntax();
            var initializer = node.Initializer;
            
            DefaultValue = initializer?.Value.ToString();
        }

        internal GeneratedProperty(string name, string typeName, string generatedCode)
        {
            Name = name;
            Type = typeName;
            GeneratedCode = generatedCode;
        }

        internal GeneratedProperty(string name, string typeName, bool isHidden, string generatedCode)
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