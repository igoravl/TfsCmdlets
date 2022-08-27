using System.Collections.Generic;
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
                if (!(declaringSyntaxReference.GetSyntax() is ClassDeclarationSyntax cds)) continue;
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
}