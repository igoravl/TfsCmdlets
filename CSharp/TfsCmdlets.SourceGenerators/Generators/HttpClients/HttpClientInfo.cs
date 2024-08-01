using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public class HttpClientInfo : GeneratorState
    {
        internal HttpClientInfo(INamedTypeSymbol symbol, GeneratorExecutionContext context, Logger logger)
            : base(symbol, logger)
        {
            OriginalType = symbol.GetAttributeConstructorValue<INamedTypeSymbol>("HttpClientAttribute");
        }

        public INamedTypeSymbol OriginalType { get; }

        internal IEnumerable<GeneratedMethod> GenerateMethods()
        {
            var methods = OriginalType
                .GetMembersRecursively(SymbolKind.Method, "Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase")
                .Cast<IMethodSymbol>()
                .Where(m => 
                    m.MethodKind == MethodKind.Ordinary && 
                    m.DeclaredAccessibility == Accessibility.Public && 
                    !m.HasAttribute("ObsoleteAttribute"))
                .ToList();

            foreach (var method in methods)
            {
                yield return new GeneratedMethod(method);
            }
        }
    }
}
