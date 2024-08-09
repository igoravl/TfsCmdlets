using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        protected Logger Logger { get; private set; }

        public void Initialize(GeneratorInitializationContext context)
        {
            Logger = new Logger(GeneratorName);

            Logger.Log($"=== Initializing generator ===");
            Logger.Log($"=== Path : {GetType().Assembly.Location}) ===");
            Logger.Log($"=== Built: {(new FileInfo(GetType().Assembly.Location)).LastWriteTime} ===");

            context.RegisterForSyntaxNotifications(() =>
            {
                var receiver = new TReceiver();
                return receiver;
            });
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is TReceiver receiver)) return;

            try
            {
                var typesToProcess = new List<(INamedTypeSymbol, TypeDeclarationSyntax)>(
                    receiver.TypesToProcess?.Values ?? Enumerable.Empty<(INamedTypeSymbol, TypeDeclarationSyntax)>());

                Logger.Log($"Preparing to generate code for {typesToProcess.Count} types");

                foreach (var (type, cds) in typesToProcess)
                {
                    Logger.Log($"{type.FullName()}");

                    //Logger.Log($" - Initializing type processor");

                    var processedType = new TProcessor();
                    processedType.Initialize(Logger, type, cds, context);

                    if (processedType.Ignore)
                    {
                        Logger.Log($" - [WARN] Ignore==true. Skipping");
                        continue;
                    }

                    if (!processedType.ClassDeclaration.IsPartial())
                    {
                        Logger.Log($" - [WARN] Type is not marked as partial. Skipping");
                        Logger.ReportDiagnostic_ClassMustBePartial(context, cds);
                        continue;
                    }

                    try
                    {
                        //Logger.Log($" - Generating code");
                        context.AddSource($"{processedType.FullName}.cs", SourceText.From(processedType.GenerateCode(), Encoding.UTF8));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, $" - Error adding source '{processedType.FullName}.cs'. Aborting");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Unexpected error while generating code");
            }
        }

        protected abstract string GeneratorName { get; }
    }
}