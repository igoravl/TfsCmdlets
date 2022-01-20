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
    public abstract class BaseGenerator<TReceiver, TProcessor> : ISourceGenerator
        where TReceiver : IFilter, new()
        where TProcessor : ITypeProcessor, new()
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            Logger.Log($"=== Initializing generator (from {GetType().Assembly.Location}) ===");
            context.RegisterForSyntaxNotifications(() => new TReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is TReceiver receiver)) return;

            try
            {
                var typesToProcess = new List<(INamedTypeSymbol, ClassDeclarationSyntax)>(
                    receiver.TypesToProcess?.Values ?? Enumerable.Empty<(INamedTypeSymbol, ClassDeclarationSyntax)>());

                Logger.Log($"Preparing to generate code for {typesToProcess.Count} types");

                foreach (var (type, cds) in typesToProcess)
                {
                    Logger.Log($"{type.FullName()}");

                    Logger.Log($" - Initializing type processor");

                    var processedType = new TProcessor();
                    processedType.Initialize(type, cds, context);

                    if (processedType.Ignore)
                    {
                        Logger.Log($" - Ignore property returned true. Skipping");
                        continue;
                    }

                    if (!processedType.ClassDeclaration.IsPartial())
                    {
                        Logger.Log($" - Type is not marked as partial. Skipping");
                        Logger.ReportDiagnostic_ClassMustBePartial(context, cds);
                        continue;
                    }

                    try
                    {
                        Logger.Log($" - Generating code");
                        context.AddSource($"{processedType.FullName}.cs", SourceText.From(processedType.GenerateCode(), Encoding.UTF8));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, $" - Error adding source '{processedType.FullName}.cs'. Aborting");
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Unexpected error while generating code");
            }
            finally
            {
                Logger.FlushLogs(context);
            }
        }
    }
}