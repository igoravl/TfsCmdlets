using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace TfsCmdlets.SourceGenerators
{
    [Generator]
    public class Generator : ISourceGenerator, ISyntaxContextReceiver
    {
        private List<INamedTypeSymbol> CmdletTypes { get; } = new List<INamedTypeSymbol>();
        private List<INamedTypeSymbol> ControllerTypes { get; } = new List<INamedTypeSymbol>();
        private Dictionary<string, GeneratorState> GeneratedTypes { get; } = new Dictionary<string, GeneratorState>();

        void ISourceGenerator.Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => this);
        }

        void ISourceGenerator.Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver == null || context.SyntaxContextReceiver.GetType() != this.GetType()) return;

            try
            {
                var generators = new List<(IGenerator, List<INamedTypeSymbol>)>()
                {
                    (new CmdletGenerator(), CmdletTypes),
                    (new ControllerGenerator(GeneratedTypes), ControllerTypes)
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

                            GeneratedTypes[state.FullName] = state;

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

        void ISyntaxContextReceiver.OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (!(context.Node is ClassDeclarationSyntax cds)) return;
            
            try
            {
                var type = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(cds);

                if (type.HasAttribute("TfsCmdletAttribute"))
                {
                    CmdletTypes.Add(type);
                }
                else if (type.HasAttribute("CmdletControllerAttribute"))
                {
                    ControllerTypes.Add(type);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
    }
}
