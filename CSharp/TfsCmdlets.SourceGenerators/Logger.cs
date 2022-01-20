using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace TfsCmdlets.SourceGenerators
{
    internal static class Logger
    {
        private static List<string> Logs { get; } = new List<string>();

        [Conditional("DEBUG")]
        internal static void Log(string message)
        {
            var sf = new StackFrame(1);
            var msg = $"[TfsCmdlets.SourceGenerators] [{DateTime.Now:HH:mm:ss.fff}] {message}";

            Logs.Add($"/* {msg} */");
            Debug.WriteLine(msg);
        }

        [Conditional("DEBUG")]
        internal static void LogError(Exception ex)
        {
            Log($"[ERROR] {ex}");
        }

        [Conditional("DEBUG")]
        internal static void LogError(Exception ex, string msg) => Log($"[ERROR] {msg} ({ex})");

        [Conditional("DEBUG")]
        internal static void FlushLogs(GeneratorExecutionContext context)
        {
            context.AddSource($"logs.g.cs", SourceText.From(string.Join("\n", Logs), Encoding.UTF8));
            Logs.Clear();
        }

        internal static void ReportDiagnostic_ClassMustBePartial(GeneratorExecutionContext context, ClassDeclarationSyntax cds)
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.ClassMustBePartial,
                cds.Identifier.GetLocation(),
                cds.Identifier
            ));
        }
    }
}
