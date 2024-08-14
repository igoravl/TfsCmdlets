using System;
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
    public class ClassMustBePartialAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(DiagnosticDescriptors.ClassMustBePartial);

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
                if (!filters.Any(filter => filter.ShouldProcessType(type))) continue;

                var error = Diagnostic.Create(DiagnosticDescriptors.ClassMustBePartial,
                    cds.Identifier.GetLocation(),
                    type.Name,
                    DiagnosticSeverity.Error);

                Debug.WriteLine($"[TfsCmdlets.Analyzer] Adding {type}");

                context.ReportDiagnostic(error);
            }
        }

        private static IEnumerable<IFilter> GetFilters()
        {
            return typeof(ClassMustBePartialAnalyzer).Assembly.GetTypes()
                .Where(t => typeof(IFilter).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IFilter>();
        }
    }

    ///// <summary>
    ///// Code fix for "Class must be partial"
    ///// </summary>
    //[ExportCodeFixProvider(LanguageNames.CSharp)]
    //public class ClassMustBePartialCodeFix : CodeFixProvider
    //{
    //    public override ImmutableArray<string> FixableDiagnosticIds { get; }
    //       = ImmutableArray.Create(DiagnosticDescriptors.ClassMustBePartial.Id);

    //    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    //    public override Task RegisterCodeFixesAsync(CodeFixContext context)
    //    {
    //        foreach (var diagnostic in context.Diagnostics)
    //        {
    //            if (diagnostic.Id != DiagnosticDescriptors.ClassMustBePartial.Id) continue;

    //            var title = DiagnosticDescriptors.ClassMustBePartial.Title.ToString();

    //            var action = CodeAction.Create(
    //                            title,
    //                            token => AddPartialKeywordAsync(context, diagnostic, token),
    //                            title);
    //            context.RegisterCodeFix(action, diagnostic);
    //        }

    //        return Task.CompletedTask;

    //    }

    //    private static async Task<Document> AddPartialKeywordAsync(
    //        CodeFixContext context,
    //        Diagnostic makePartial,
    //        CancellationToken cancellationToken)
    //    {
    //        var root = await context.Document.GetSyntaxRootAsync(cancellationToken);

    //        if (root is null) return context.Document;

    //        var classDeclaration = FindClassDeclaration(makePartial, root);
    //        var partial = SyntaxFactory.Token(SyntaxKind.PartialKeyword);
    //        var newDeclaration = classDeclaration.AddModifiers(partial);
    //        var newRoot = root.ReplaceNode(classDeclaration, newDeclaration);
    //        var newDoc = context.Document.WithSyntaxRoot(newRoot);

    //        return newDoc;
    //    }

    //    private static TypeDeclarationSyntax FindClassDeclaration(
    //       Diagnostic makePartial,
    //       SyntaxNode root)
    //    {
    //        var diagnosticSpan = makePartial.Location.SourceSpan;

    //        return root.FindToken(diagnosticSpan.Start)
    //                   .Parent?.AncestorsAndSelf()
    //                   .OfType<TypeDeclarationSyntax>()
    //                   .First();
    //    }
    //}
}
