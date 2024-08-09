using System.Collections.Generic;
using System.Diagnostics;
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
            Methods = OriginalType
                .GetMembersRecursively(SymbolKind.Method, "Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase")
                .Cast<IMethodSymbol>()
                .Where(m =>
                    m.MethodKind == MethodKind.Ordinary &&
                    m.DeclaredAccessibility == Accessibility.Public &&
                    !m.IsOverride &&
                    !m.HasAttribute("ObsoleteAttribute"))
                .ToList();
        }

        public INamedTypeSymbol OriginalType { get; }
        public IEnumerable<IMethodSymbol> Methods { get; }

        internal IEnumerable<GeneratedMethod> GenerateMethods()
        {
            foreach (var method in Methods)
            {
                yield return new GeneratedMethod(method);
            }
        }
    }
}
