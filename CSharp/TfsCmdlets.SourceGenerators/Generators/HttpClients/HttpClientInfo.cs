using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public record HttpClientInfo : ClassInfoBase
    {
        public string OriginalType { get; private set; }

        public EquatableArray<MethodInfo> Methods { get; }

        public HttpClientInfo(INamedTypeSymbol symbol)
            : base(symbol)
        {
            OriginalType = symbol.GetAttributeConstructorValue<INamedTypeSymbol>("HttpClientAttribute").FullName();
            Methods = GetMethods();
        }

        public static HttpClientInfo Create(GeneratorAttributeSyntaxContext ctx)
        {
            if (ctx.TargetSymbol is not INamedTypeSymbol namedTypeSymbol) return null;
            return new HttpClientInfo(namedTypeSymbol);
        }
    } 
}
        //    OriginalType = symbol.GetAttributeConstructorValue<INamedTypeSymbol>("HttpClientAttribute").FullName();
        //    Methods = OriginalType
        //        .GetMembersRecursively(SymbolKind.Method, "Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase")
        //        .Cast<IMethodSymbol>()
        //        .Where(m =>
        //            m.MethodKind == MethodKind.Ordinary &&
        //            m.DeclaredAccessibility == Accessibility.Public &&
        //            !m.IsOverride &&
        //            !m.HasAttribute("ObsoleteAttribute"))
        //        .ToList();
        //}

        //internal IEnumerable<GeneratedMethod> GenerateMethods()
        //{
        //    foreach (var method in Methods)
        //    {
        //        yield return new GeneratedMethod(method);
        //    }
        //}

        //private string GetInterfaceBody()
        //{
        //    var sb = new StringBuilder();

        //    foreach (var method in GenerateMethods())
        //    {
        //        sb.Append($"\t\t{method}");
        //        sb.AppendLine(";");
        //    }

        //    return sb.ToString();
        //}

        //private string GetClassBody()
        //{
        //    var sb = new StringBuilder();

        //    foreach (var method in GenerateMethods())
        //    {
        //        sb.Append($"\t\t{method.ToString($"\t\t\t=> Client.{method.Name}{method.SignatureNamesOnly};")}");
        //    }

        //    return sb.ToString();
        //}

        //public override string GenerateCode()
        //{
        //    return string.Empty;
            //return $$"""
            //        using System.Composition;
                    
            //        namespace {{Namespace}}
            //        {
            //            public partial interface {{Name}}: IVssHttpClient
            //            {
            //        {{GetInterfaceBody()}}
            //            }
                        
            //            [Export(typeof({{Name}}))]
            //            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            //            internal class {{Name}}Impl: {{Name}}
            //            {
            //                private {{OriginalType}} _client;
                            
            //                protected IDataManager Data { get; }
                            
            //                [ImportingConstructor]
            //                public {{Name}}Impl(IDataManager data)
            //                {
            //                    Data = data;
            //                }
                            
            //                private {{OriginalType}} Client
            //                {
            //                    get
            //                    {
            //                        if(_client == null)
            //                        {
            //                            _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof({{OriginalType}})) as {{OriginalType}};
            //                        }
            //                        return _client;
            //                    }
            //                }
                            
            //        {{GetClassBody()}}
            //            }
            //        }
            //        """;
        //}
//    }
//}
