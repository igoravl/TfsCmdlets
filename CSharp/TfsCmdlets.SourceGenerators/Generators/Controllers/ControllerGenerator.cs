using System.Text;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using TfsCmdlets.SourceGenerators.Generators.Cmdlets;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    [Generator]
    public class ControllerGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var cmdletsToGenerate = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "TfsCmdlets.Cmdlets.TfsCmdletAttribute",
                    predicate: (_, _) => true,
                    transform: static (ctx, _) => CmdletInfo.Create(ctx))
                .Where(static m => m is not null)
                .Select((m, _) => m!)
                .Collect();

            var controllersToGenerate = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "TfsCmdlets.CmdletControllerAttribute",
                    predicate: (_, _) => true,
                    transform: static (ctx, _) => ControllerInfo.Create(ctx))
                .Where(static m => m is not null)
                .Select((m, _) => m!)
                .Combine(cmdletsToGenerate);

            context.RegisterSourceOutput(controllersToGenerate,
                static (spc, source) =>
                {
                    var controller = source.Left;
                    var cmdlet = source.Right.OfType<CmdletInfo>().FirstOrDefault(c => c.Name.Equals(controller.CmdletName));
                    var result = controller.GenerateCode(cmdlet);
                    var filename = controller.FileName;
                    spc.AddSource(filename, SourceText.From(result, Encoding.UTF8));
                });
        }
    }
}