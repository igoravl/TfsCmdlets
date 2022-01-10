using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace TfsCmdlets.SourceGenerators
{
    internal static class Logger
    {
        private static List<string> Logs { get; } = new List<string>();

        internal static void Log(string message)
        {
            var msg = $"[{DateTime.Now.ToString("HH:mm:ss.fff")}] {message}";
            
            Logs.Add($"/* {msg} */");
            Debug.WriteLine($"[TfsCmdlets.Generator] {msg}");
        }

        internal static void LogError(Exception ex) => Log($"[ERROR] {ex}");

        internal static void FlushLogs(GeneratorExecutionContext context)
        {
            context.AddSource($"logs.g.cs", SourceText.From(string.Join("\n", Logs), Encoding.UTF8));
            Logs.Clear();
        }
    }
}
