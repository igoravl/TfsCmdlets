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

        public string GetInterfaceBody()
        {
            var sb = new StringBuilder();

            foreach (var method in OriginalType.Methods)
            {
                sb.Append($"\t\t{method}");
                sb.AppendLine(";");
            }

            return sb.ToString();
        }

        public string GetClassBody()
        {
            var sb = new StringBuilder();

            foreach (var method in OriginalType.Methods)
            {
                sb.Append($"\t\t{method.ToString($"\t\t\t=> Client.{method.Name}{method.SignatureNamesOnly};")}");
            }

            return sb.ToString();
        }
    }
} 
