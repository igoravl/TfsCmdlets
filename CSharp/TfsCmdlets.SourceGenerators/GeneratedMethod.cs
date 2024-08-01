using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    internal record GeneratedMethod
    {
        public GeneratedMethod(IMethodSymbol method)
        {
            var parms1 = new List<string>();
            var parms2 = new List<string>();
            
            Name = method.Name;
            ReturnType = method.ReturnType;

            foreach (var p in method.Parameters)
            {
                var defaultValue = p.HasExplicitDefaultValue ? $" = {p.ExplicitDefaultValue?? (p.Type.IsValueType? $"default({p.Type.ToDisplayString()})" : "null")}" : string.Empty;
                parms1.Add($"{p.Type.FullName()} {p.Name}{defaultValue}");
                parms2.Add(p.Name);
            }

            Signature = $"({string.Join(", ", parms1)})";
            SignatureNamesOnly = $"({string.Join(", ", parms2)})";
        }

        public string SignatureNamesOnly { get; set; }

        public string Name { get; } 
        
        public string Signature { get; } 
        
        public ITypeSymbol ReturnType { get; }

        public override string ToString()
        {
            return ToString(null);
        }
        
        public string ToString(string body)
        {
            var sb = new StringBuilder();
            sb.Append($"public {ReturnType.FullName()} {Name}{Signature}");

            if (string.IsNullOrWhiteSpace(body)) return sb.ToString();

            var isMethodBody = body.TrimStart().StartsWith("=>");
            
            sb.AppendLine();
            
            if(!isMethodBody) sb.AppendLine("{");
            sb.AppendLine(body);
            if (!isMethodBody) sb.AppendLine("}");
            
            return sb.ToString();
        }
    };
}
