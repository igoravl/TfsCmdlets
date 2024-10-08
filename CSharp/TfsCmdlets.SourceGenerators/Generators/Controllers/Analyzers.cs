﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace TfsCmdlets.SourceGenerators.Analyzers
{
    /// <summary>
    /// Analyzer for "Class must be partial"
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ClassMustHaveControllerSuffix : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DiagnosticDescriptors.ClassMustHaveControllerSuffix);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(AnalyzeNamedType, SymbolKind.NamedType);
        }

        private static void AnalyzeNamedType(SymbolAnalysisContext context)
        {
            var type = (INamedTypeSymbol)context.Symbol;
            var filters = GetFilters().ToList();

            foreach (var declaringSyntaxReference in type.DeclaringSyntaxReferences)
            {
                if (!(declaringSyntaxReference.GetSyntax() is TypeDeclarationSyntax cds)) continue;
                if (cds.IsPartial()) continue;
                if (!filters.Any(filter => filter.ShouldProcessType(type) && !type.Name.EndsWith("Controller"))) continue;

                var error = Diagnostic.Create(DiagnosticDescriptors.ClassMustHaveControllerSuffix,
                    cds.Identifier.GetLocation(),
                    type.Name,
                    DiagnosticSeverity.Error);

                Debug.WriteLine($"[TfsCmdlets.Analyzer] Adding {type}");

                context.ReportDiagnostic(error);
            }
        }

        private static IEnumerable<IFilter> GetFilters()
        {
            yield return new Generators.Controllers.Filter();
        }
    }

    /// <summary>
    /// Analyzer for "Client must be interface"
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ClientMustBeInterface : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DiagnosticDescriptors.ClientMustBeInterface);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(AnalyzeNamedType, SymbolKind.NamedType);
        }

        private static void AnalyzeNamedType(SymbolAnalysisContext context)
        {
            var type = (INamedTypeSymbol)context.Symbol;
            var filters = GetFilters().ToList();

            foreach (var declaringSyntaxReference in type.DeclaringSyntaxReferences)
            {
                if (declaringSyntaxReference.GetSyntax() is not TypeDeclarationSyntax cds) continue;
                if (!filters.Any(filter => filter.ShouldProcessType(type))) continue;

                var clientType = type.GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "Client");
                if(clientType == null) continue;

                if ((clientType.TypeKind == TypeKind.Interface) && clientType.Interfaces.Any(i => i.Name.Equals("IVssHttpClient"))) continue;

                var error = Diagnostic.Create(DiagnosticDescriptors.ClientMustBeInterface,
                    cds.Identifier.GetLocation(),
                    clientType.Name,
                    DiagnosticSeverity.Error);

                Debug.WriteLine($"[TfsCmdlets.Analyzer] Adding {type}");

                context.ReportDiagnostic(error);
            }
        }

        private static IEnumerable<IFilter> GetFilters()
        {
            yield return new Generators.Controllers.Filter();
        }
    }
}