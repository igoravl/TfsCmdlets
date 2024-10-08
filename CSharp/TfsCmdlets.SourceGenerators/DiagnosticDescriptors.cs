﻿using System;
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

        internal static DiagnosticDescriptor ParameterMustBeProperty { get; } = new DiagnosticDescriptor(
            "TFS003",
            "Parameter must be property", "'{0}' is marked with [Parameter] attribute, but it is a field. Parameters must be properties.",
            "CodeGen",
            DiagnosticSeverity.Error,
            true);

        internal static DiagnosticDescriptor ClientMustBeInterface { get; } = new DiagnosticDescriptor(
            "TFS004",
            "HttpClient attribute must refer to a valid interface", "'{0}' must be an interface, derived from TfsCmdlets.HttpClients.IVssHttpClient.",
            "CodeGen",
            DiagnosticSeverity.Error,
            true);

        internal static DiagnosticDescriptor ParameterMustHaveComments { get; } = new DiagnosticDescriptor(
            "TFS005",
            "Parameter must have XML comments", "Parameter '{0}' must have at least <summary> XML comments to enable command-line help",
            "CodeGen",
            DiagnosticSeverity.Error,
            true);
    }
}
