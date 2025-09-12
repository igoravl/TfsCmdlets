using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public record HttpClientInfo : ClassInfo
    {
        public ClassInfo OriginalType { get; private set; }

        public HttpClientInfo(INamedTypeSymbol symbol)
            : base(symbol)
        {
            var originalType = symbol.GetAttributeConstructorValue<INamedTypeSymbol>("HttpClientAttribute");
            OriginalType = new ClassInfo(originalType, false, true, true, true,
                "Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase");
        }

        public static HttpClientInfo Create(GeneratorAttributeSyntaxContext ctx)
        {
            return ctx.TargetSymbol is not INamedTypeSymbol namedTypeSymbol ? 
                null : 
                new HttpClientInfo(namedTypeSymbol);
        }

        private string GetInterfaceBody()
        {
            var sb = new StringBuilder();

            foreach (var method in OriginalType.Methods)
            {
                sb.Append($"\t\t{method}");
                sb.AppendLine(";");
            }

            return sb.ToString();
        }

        private string GetClassBody()
        {
            var sb = new StringBuilder();

            foreach (var method in OriginalType.Methods)
            {
                sb.Append($"\t\t{method.ToString($"\t\t\t=> Client.{method.Name}{method.SignatureNamesOnly};")}");
            }

            return sb.ToString();
        }


        public override string GenerateCode()
        {
            return $$"""
                     using System.Composition;
                     {{UsingsStatements}}

                     namespace {{Namespace}}
                     {
                         public partial interface {{Name}}: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
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
