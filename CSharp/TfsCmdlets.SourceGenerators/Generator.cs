using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace TfsCmdlets.SourceGenerators
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        private IDictionary<string, GeneratorState> _generatedTypes;

        void ISourceGenerator.Initialize(GeneratorInitializationContext context)
        {
            Logger.Log("=== Initializing generator ===");
            _generatedTypes = new Dictionary<string, GeneratorState>();
            context.RegisterForSyntaxNotifications(() => new SyntaxContextReceiver());
        }

        void ISourceGenerator.Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is SyntaxContextReceiver receiver)) return;

            try
            {
                var generators = new List<(IGenerator, ICollection<INamedTypeSymbol>)>()
                {
                    (new CmdletGenerator(), receiver.CmdletTypes.Values),
                    (new ControllerGenerator(_generatedTypes), receiver.ControllerTypes.Values)
                };

                foreach (var (generator, types) in generators)
                {
                    var generatorName = generator.GetType().Name;

                    Logger.Log($"Initializing generator {generatorName}");

                    generator.Initialize(context);

                    foreach (var type in types)
                    {
                        Logger.Log($"{generatorName}: Processing '{type.FullName()}'");

                        try
                        {
                            var state = generator.ProcessType(context, type);
                            if (state == null) continue;

                            _generatedTypes[state.FullName] = state;

                            context.AddSource($"{state.FileName}.cs",
                                SourceText.From(generator.Generate(state), Encoding.UTF8));
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                Logger.FlushLogs(context);
            }
        }

        private class SyntaxContextReceiver : ISyntaxContextReceiver
        {
            internal IDictionary<string, INamedTypeSymbol> CmdletTypes { get; set; } = new Dictionary<string, INamedTypeSymbol>();
            internal IDictionary<string, INamedTypeSymbol> ControllerTypes { get; set; } = new Dictionary<string, INamedTypeSymbol>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (!(context.Node is ClassDeclarationSyntax cds)) return;

                try
                {
                    var type = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(cds);

                    Logger.Log($"Visiting class '{type.FullName()}'");

                    if (type.HasAttribute("TfsCmdletAttribute"))
                    {
                        if (CmdletTypes.ContainsKey(type.FullName()))
                        {
                            Logger.Log($"[WARN] Type '{type.FullName()}' already registered. Skipping.");
                            return;
                        }

                        CmdletTypes.Add(type.FullName(), type);
                    }
                    else if (type.HasAttribute("CmdletControllerAttribute"))
                    {
                        if (ControllerTypes.ContainsKey(type.FullName()))
                        {
                            Logger.Log($"[WARN] Type '{type.FullName()}' already registered. Skipping.");
                            return;
                        }

                        ControllerTypes.Add(type.FullName(), type);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }
        }
    }
}
