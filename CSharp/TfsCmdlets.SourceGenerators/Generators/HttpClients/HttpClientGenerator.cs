using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using TfsCmdlets.SourceGenerators.Generators.Cmdlets;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public class HttpClientGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var clientsToGenerate = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "TfsCmdlets.HttpClients.HttpClientAttribute",
                    predicate: (_, _) => true,
                    transform: static (ctx, _) => HttpClientInfo.Create(ctx))
                .Where(static m => m is not null)
                .Select((m, _) => m!);

            context.RegisterSourceOutput(clientsToGenerate,
                static (spc, source) =>
                {
                    var result = source.GenerateCode();
                    var filename = source.FileName;
                    spc.AddSource(filename, SourceText.From(result, Encoding.UTF8));
                });
        }
    }
}
