using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace TfsCmdlets.SourceGenerators
{
    [Generator]
    public class ImportingConstructorGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ImportingConstructorSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is ImportingConstructorSyntaxReceiver syntaxReceiver)) return;

            foreach (var controller in syntaxReceiver.Controllers)
            {
                var genericArg = "";
                var ctorArgs = "";
                var baseCtorArgs= "";
                var propInit = "";

                context.AddSource($"{controller.FullName()}.cs", SourceText.From($@"/*
Controller name: {controller.FullName()}
*/


using TfsCmdlets.Services;

// ReSharper disable once CheckNamespace
namespace {controller.FullNamespace()}
{{
        internal partial class {controller.Name}: {controller.BaseType.Name}{genericArg}
  {{
            [System.Composition.ImportingConstructor]
            internal {controller.Name}({ctorArgs})
                : base({baseCtorArgs})
    {{
{propInit}
    }}
}}
}}
", 
                    Encoding.UTF8));
            }
        }

        internal class ImportingConstructorSyntaxReceiver : ISyntaxContextReceiver
        {
            internal INamedTypeSymbol ControllerBase { get; private set; }
            internal INamedTypeSymbol ControllerBaseT { get; private set; }
            internal List<INamedTypeSymbol> Controllers { get; } = new List<INamedTypeSymbol>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                var node = context.Node;

                if (!(node is ClassDeclarationSyntax cds)) return;

                var type = context.SemanticModel.GetDeclaredSymbol(cds) as INamedTypeSymbol;

                if (type.Name.EndsWith("Controller"))
                {
                    Controllers.Add(type);
                    return;
                }

                if (type.Name.Equals("ControllerBase"))
                {
                    if (type.IsGenericType)
                    {
                        ControllerBaseT = type;
                        return;
                    }

                    ControllerBase = type;
                }
            }
        }
    }
}