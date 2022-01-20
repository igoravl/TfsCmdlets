using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    internal static class DiagnosticDescriptors
    {
        internal static DiagnosticDescriptor ClassMustBePartial { get; } = new DiagnosticDescriptor(
            "TFS001",
            "Class must be partial", "Add partial' modifier to class '{0}' to enable code generation",
            "CodeGen",
            DiagnosticSeverity.Error,
            true);
    }
}
