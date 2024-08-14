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
    public class ParameterMustHaveComments: DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DiagnosticDescriptors.ParameterMustHaveComments);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(AnalyzeProperty, SymbolKind.Property);
        }

        private static void AnalyzeProperty(SymbolAnalysisContext context)
        {
            if (context.Symbol is not IPropertySymbol prop) return;

            if ((prop.DeclaredAccessibility & Accessibility.Public) != Accessibility.Public) return;
            
            foreach (var dsr in prop.DeclaringSyntaxReferences)
            {
                if (dsr.GetSyntax() is not PropertyDeclarationSyntax pds) continue;

                var attr = pds.AttributeLists.FirstOrDefault(a => a.Attributes.Any(a2 => a2.Name.ToString() == "Parameter"));

                if (attr == null) continue;

                if (pds.HasXmlCommentTag("summary")) continue;
                
                var error = Diagnostic.Create(DiagnosticDescriptors.ParameterMustHaveComments,
                    pds.GetLocation(),
                    prop.Name,
                    DiagnosticSeverity.Error);

                context.ReportDiagnostic(error);
            }
        }
    }
}
