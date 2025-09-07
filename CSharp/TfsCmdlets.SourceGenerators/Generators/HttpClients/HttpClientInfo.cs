using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public record HttpClientInfo : GeneratorState
    {
        internal HttpClientInfo(INamedTypeSymbol symbol, GeneratorExecutionContext context)
            : base(symbol)
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

        private string GetInterfaceBody()
        {
            var sb = new StringBuilder();

            foreach (var method in GenerateMethods())
            {
                sb.Append($"\t\t{method}");
                sb.AppendLine(";");
            }

            return sb.ToString();
        }

        private string GetClassBody()
        {
            var sb = new StringBuilder();

            foreach (var method in GenerateMethods())
            {
                sb.Append($"\t\t{method.ToString($"\t\t\t=> Client.{method.Name}{method.SignatureNamesOnly};")}");
            }

            return sb.ToString();
        }

        public override string GenerateCode()
        {
            return $$"""
                    using System.Composition;
                    
                    namespace {{Namespace}}
                    {
                        public partial interface {{Name}}: IVssHttpClient
                        {
                    {{GetInterfaceBody()}}
                        }
                        
                        [Export(typeof({{Name}}))]
                        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
                        internal class {{Name}}Impl: {{Name}}
                        {
                            private {{OriginalType}} _client;
                            
                            protected IDataManager Data { get; }
                            
                            [ImportingConstructor]
                            public {{Name}}Impl(IDataManager data)
                            {
                                Data = data;
                            }
                            
                            private {{OriginalType}} Client
                            {
                                get
                                {
                                    if(_client == null)
                                    {
                                        _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof({{OriginalType}})) as {{OriginalType}};
                                    }
                                    return _client;
                                }
                            }
                            
                    {{GetClassBody()}}
                        }
                    }
                    """;
        }
    }
}
