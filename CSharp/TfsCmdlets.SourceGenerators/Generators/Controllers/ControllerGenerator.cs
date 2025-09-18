using System.Text;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                    "TfsCmdlets.TfsCmdletAttribute",
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
                    var allCmdlets = source.Right.OfType<CmdletInfo>().ToList();
                    var cmdlet = allCmdlets.FirstOrDefault(c => c.Name.Equals(controller.CmdletName));
                    var result = GenerateCode(controller, cmdlet);
                    var filename = controller.FileName;
                    spc.AddSource(filename, SourceText.From(result, Encoding.UTF8));
                });
        }

        private static string GenerateCode(ControllerInfo model, CmdletInfo cmdlet)
        {
            model.CmdletInfo = cmdlet;

            return $$"""
                     {{model.GenerateUsings()}}

                     namespace {{model.Namespace}}
                     {
                         internal partial class {{model.Name}}: {{model.BaseClassName}}
                         {
                     {{model.GenerateClientProperty()}}{{model.GenerateGetInputProperty()}}{{model.GenerateDeclaredProperties()}}{{model.GenerateAutomaticProperties()}}{{model.GenerateScopeProperties()}}
                             // ParameterSetName
                             protected bool Has_ParameterSetName { get; set; }
                             protected string ParameterSetName { get; set; }
                     {{model.GenerateItemsProperty()}}
                             // DataType
                             public override Type DataType => typeof({{model.DataType}});

                             protected override void CacheParameters()
                             {
                     {{model.GenerateCacheProperties()}}
                             }

                             [ImportingConstructor]
                             public {{model.Name}}({{model.ImportingConstructorArgs}})
                                 : base({{model.ImportingBaseArgs}})
                             {
                     {{model.ImportingConstructorBody}}
                             }
                         }
                     }

                     """;
        }

    }
}