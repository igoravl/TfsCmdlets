using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    [Generator]
    public class CmdletGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var cmdletsToGenerate = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "TfsCmdlets.Cmdlets.TfsCmdletAttribute",
                    predicate: (_, _) => true,
                    transform: static (ctx, _) => CmdletInfo.Create(ctx))
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