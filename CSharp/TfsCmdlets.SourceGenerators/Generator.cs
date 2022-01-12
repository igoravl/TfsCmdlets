﻿using System;
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
        private IDictionary<string, INamedTypeSymbol> CmdletTypes { get; } = new Dictionary<string, INamedTypeSymbol>();
        private IDictionary<string, INamedTypeSymbol> ControllerTypes { get; } = new Dictionary<string, INamedTypeSymbol>();
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
                var generators = new List<(IGenerator, ICollection<INamedTypeSymbol>)>()
                {
                    (new CmdletGenerator(), CmdletTypes.Values),
                    (new ControllerGenerator(GeneratedTypes), ControllerTypes.Values)
                };

                foreach (var (generator, types) in generators)
                {
                    var generatorName = generator.GetType().Name;

                    Logger.Log($"Initializing generator {generatorName}");

                    generator.Initialize(context);

                    foreach (var type in types)
                    {
                        if(GeneratedTypes.ContainsKey(type.FullName()))
                        {
                            Logger.Log("[WARN] Type already generated. Skipping.");
                            continue;
                        }

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

                Logger.Log($"Visiting class '{type.FullName()}'");

                if (type.HasAttribute("TfsCmdletAttribute"))
                {
                    if(CmdletTypes.ContainsKey(type.FullName()))
                    {
                        Logger.Log($"[WARN] Type '{type.FullName()}' already registered. Skipping.");
                        return;
                    }

                    CmdletTypes.Add(type.FullName(), type);
                }
                else if (type.HasAttribute("CmdletControllerAttribute"))
                {
                    if(ControllerTypes.ContainsKey(type.FullName()))
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
