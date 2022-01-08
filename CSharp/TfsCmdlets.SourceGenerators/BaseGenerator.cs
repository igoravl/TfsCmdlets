using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace TfsCmdlets.SourceGenerators
{
    public abstract class BaseGenerator : ISourceGenerator, ISyntaxContextReceiver
    {
        protected List<INamedTypeSymbol> Types { get; } = new List<INamedTypeSymbol>();

        protected abstract string AttributeName { get; }

        void ISourceGenerator.Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => this);
        }

        void ISourceGenerator.Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver == null || context.SyntaxContextReceiver.GetType() != this.GetType()) return;

            OnBeforeGenerate(context);

            foreach (var type in this.Types)
            {
                Logger.Log($"Processing {type.FullName()}");

                try
                {
                    var state = ProcessType(context, type);

                    if(state == null) continue;

                    context.AddSource($"{state.FileName}.cs", SourceText.From(GetGeneratedText(state), Encoding.UTF8));
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            Logger.FlushLogs(context);
        }

        protected virtual void OnBeforeGenerate(GeneratorExecutionContext context) { }

        protected abstract GeneratorState ProcessType(GeneratorExecutionContext context, INamedTypeSymbol type);

        protected abstract string GetGeneratedText(GeneratorState state);

        protected virtual bool ShouldSkip(INamedTypeSymbol type) => false;

        void ISyntaxContextReceiver.OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            var node = context.Node;

            if (!(node is ClassDeclarationSyntax cds)) return;

            var type = (INamedTypeSymbol) context.SemanticModel.GetDeclaredSymbol(cds);

            if (type == null || !type.GetAttributes().Any(a => a.AttributeClass.Name.Equals(AttributeName)) || ShouldSkip(type)) return;

            Types.Add(type);
        }
    }
}
