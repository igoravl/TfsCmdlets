using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators
{
    public class Logger
    {
        private readonly Guid _instanceId = Guid.NewGuid();
        private readonly string _generatorName;

        public Logger(string generatorName)
        {
            _generatorName = generatorName;
        }

        [Conditional("DEBUG")]
        internal void Log(string message) => Debug.WriteLine($"[TfsCmdlets.SourceGenerators {_instanceId}] [{_generatorName}] {message}");

        [Conditional("DEBUG")]
        internal void LogError(Exception ex) => Log($"[ERROR] {ex}");

        [Conditional("DEBUG")]
        internal void LogError(Exception ex, string msg) => Log($"[ERROR] {msg} ({ex})");

        internal void ReportDiagnostic_ClassMustBePartial(GeneratorExecutionContext context, TypeDeclarationSyntax cds)
            => context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.ClassMustBePartial,
                cds.Identifier.GetLocation(),
                cds.Identifier));

        internal void ReportDiagnostic(DiagnosticDescriptor descriptor, GeneratorExecutionContext context, ISymbol symbol)
            => ReportDiagnostic(descriptor, context, symbol.DeclaringSyntaxReferences.First().GetSyntax().GetLocation(), symbol.Name);
            
        internal void ReportDiagnostic(DiagnosticDescriptor descriptor, GeneratorExecutionContext context, Location location, params object[] args)
            => context.ReportDiagnostic(Diagnostic.Create(descriptor, location, args));
    }
}
