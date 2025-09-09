using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using TfsCmdlets.SourceGenerators.Generators.Cmdlets;

namespace TfsCmdlets.SourceGenerators.Generators.Models
{
    public class ModelGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var cmdletsToGenerate = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "TfsCmdlets.ModelAttribute",
                    predicate: (_, _) => true,
                    transform: static (ctx, _) => ModelInfo.Create(ctx))
                .Where(static m => m is not null)
                .Select((m, _) => m!);

            context.RegisterSourceOutput(cmdletsToGenerate,
                static (spc, source) =>
                {
                    var result = source.GenerateCode();
                    var filename = source.FileName;
                    spc.AddSource(filename, SourceText.From(result, Encoding.UTF8));
                });
        }
    }
}
