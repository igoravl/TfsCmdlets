using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class ImportingConstructorGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new ImportingConstructorSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        context.AddSource($@"
/*
{DateTime.Now}
*/
", "Temp");
    }

    internal class ImportingConstructorSyntaxReceiver : ISyntaxContextReceiver
    {
        internal static INamedTypeSymbol ControllerBase { get; private set; }
        internal static INamedTypeSymbol ControllerBaseT { get; private set; }

        internal List<INamedTypeSymbol> Controllers = new List<INamedTypeSymbol>();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            var node = context.Node;

            if (!(node is ClassDeclarationSyntax cds)) return;

            var type = context.SemanticModel.GetDeclaredSymbol(cds) as INamedTypeSymbol;

            if (type.BaseType.Name.Equals("ControllerBase"))
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