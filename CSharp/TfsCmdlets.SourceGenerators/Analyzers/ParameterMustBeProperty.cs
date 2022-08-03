using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace TfsCmdlets.SourceGenerators.Analyzers
{
    /// <summary>
    /// Analyzer for "Parameter must be property"
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ParameterMustBeProperty : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DiagnosticDescriptors.ParameterMustBeProperty);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(AnalyzeField, SymbolKind.Field);
        }

        private static void AnalyzeField(SymbolAnalysisContext context)
        {
            if (!(context.Symbol is IFieldSymbol field)) return;

            foreach (var dsr in field.DeclaringSyntaxReferences)
            {
                if (!(dsr.GetSyntax() is FieldDeclarationSyntax fds)) continue;

                var attr = fds.AttributeLists.FirstOrDefault(a => a.Attributes.Any(a2 => a2.Name.ToString() == "Parameter"));

                if (attr == null) continue;

                var error = Diagnostic.Create(DiagnosticDescriptors.ParameterMustBeProperty,
                    fds.Declaration.GetLocation(),
                    field.Name,
                    DiagnosticSeverity.Error);

                context.ReportDiagnostic(error);
            }
        }
    }
}
