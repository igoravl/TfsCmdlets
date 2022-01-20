using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators
{
    public abstract class BaseFilter : IFilter
    {
        public IDictionary<string, (INamedTypeSymbol, ClassDeclarationSyntax)> TypesToProcess { get; } = new Dictionary<string, (INamedTypeSymbol, ClassDeclarationSyntax)>();

        public abstract bool ShouldProcessType(INamedTypeSymbol type);

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (!(context.Node is ClassDeclarationSyntax cds)) return;

            try
            {
                var type = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(cds);

                if (type == null)
                {
                    Logger.LogError(new Exception($"Unexpected error: Type {cds.Identifier} not found."));
                    return;
                }

                if (!ShouldProcessType(type)) return;

                Logger.Log($"Found '{type.FullName()}'");
                TypesToProcess[type.FullName()] = (type, cds);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
    }
}