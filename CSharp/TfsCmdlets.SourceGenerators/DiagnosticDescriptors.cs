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

        internal static DiagnosticDescriptor ClassMustHaveControllerSuffix { get; } = new DiagnosticDescriptor(
            "TFS002",
            "Class must have 'Controller' suffix", "Class '{0}' should be called '{0}Controller'",
            "CodeGen",
            DiagnosticSeverity.Error,
            true);
    }
}
