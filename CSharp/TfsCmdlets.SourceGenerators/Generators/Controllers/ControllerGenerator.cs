using System;
using System.Collections;
using System.Collections.Generic;
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
            try
            {
                var cmdletsToGenerate = context.SyntaxProvider
                    .ForAttributeWithMetadataName(
                        "TfsCmdlets.TfsCmdletAttribute",
                        predicate: (_, _) => true,
                        transform: static (ctx, _) => CmdletInfo.Create(ctx))
                    .Where(static m => m is not null)
                    .Select((m, _) => m!)
                    .Collect();

                var baseClasses = context.SyntaxProvider
                    .ForAttributeWithMetadataName(
                        "TfsCmdlets.CmdletControllerAttribute",
                        predicate: (n, _) => (n as ClassDeclarationSyntax)?
                            .AttributeLists.SelectMany(attrList => attrList.Attributes)
                            .Any(attr => (attr.Name as IdentifierNameSyntax)?.Identifier.Text == "CmdletController") ?? false,
                        transform: static (ctx, _) => ClassInfo.CreateFromAttributeValue(ctx, "CmdletControllerAttribute", "CustomBaseClass"))
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
                    .Combine(cmdletsToGenerate)
                    .Combine(baseClasses);

                context.RegisterSourceOutput(controllersToGenerate,
                    static (spc, source) =>
                    {
                        try
                        {
                            var controller = source.Left.Left;
                            var baseClasses = source.Right.ToList();
                            var baseClass = baseClasses.FirstOrDefault(ci => ci.FullName == controller.BaseClassFullName);
                            var allCmdlets = source.Left.Right.ToList();
                            var cmdlet = allCmdlets.FirstOrDefault(c => c.Name.Equals(controller.CmdletName));
                            var result = GenerateCode(controller, cmdlet, allCmdlets, baseClass);
                            var filename = controller.FileName;
                            spc.AddSource(filename, SourceText.From(result, Encoding.UTF8));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Error generating {source.Left.Left.Name}: {ex}", ex);
                        }
                    });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error initializing generator: {ex}", ex);
            }
        }

        private static string GenerateCode(ControllerInfo model, CmdletInfo cmdlet, IList<CmdletInfo> allCmdlets, ClassInfo baseClass)
        {
            model.CmdletInfo = string.IsNullOrEmpty(model.CustomCmdletName)
                ? cmdlet
                : allCmdlets.First(c => c.Name == model.CustomCmdletName);
            model.SetBaseClass(baseClass);

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
                     {{model.GenerateItemsProperty()}}{{model.GenerateDataTypeProperty()}}
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