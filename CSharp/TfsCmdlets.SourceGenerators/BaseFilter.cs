using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators
{
    public abstract class BaseFilter : IFilter
    {
        public IDictionary<string, (INamedTypeSymbol, TypeDeclarationSyntax)> TypesToProcess { get; } = new Dictionary<string, (INamedTypeSymbol, TypeDeclarationSyntax)>();

        public abstract bool ShouldProcessType(INamedTypeSymbol type);

        protected Logger Logger { get; private set; }

        public void Initialize(Logger logger)
        {
            Logger = logger;
        }

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (!(context.Node is TypeDeclarationSyntax tds)) return;

            try
            {
                var type = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(tds);

                if (type == null)
                {
                    Logger.LogError(new Exception($"Unexpected error: Type {tds.Identifier} not found."));
                    return;
                }

                if (!ShouldProcessType(type)) return;

                //Logger.Log($"Found '{type.FullName()}'");

                TypesToProcess[type.FullName()] = (type, tds);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
    }
}