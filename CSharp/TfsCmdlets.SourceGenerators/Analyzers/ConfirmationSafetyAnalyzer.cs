using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using TfsCmdlets.SourceGenerators.Generators.Cmdlets;

namespace TfsCmdlets.SourceGenerators.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class ConfirmationSafetyAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(
                DiagnosticDescriptors.ControllerMustCallShouldProcess,
                DiagnosticDescriptors.ConfirmationResultMustBeUsed,
                DiagnosticDescriptors.ConfirmationSupportMustBeEnabled);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterCompilationStartAction(start =>
            {
                var controllers = new ConcurrentBag<INamedTypeSymbol>();
                var invocations = new ConcurrentBag<(IInvocationOperation Invocation, ISymbol Owner)>();
                var calls = new ConcurrentDictionary<ISymbol, ConcurrentDictionary<ISymbol, byte>>(
                    SymbolEqualityComparer.Default);
                start.RegisterSymbolAction(symbol => controllers.Add((INamedTypeSymbol)symbol.Symbol),
                    SymbolKind.NamedType);
                start.RegisterOperationAction(operation =>
                {
                    var invocation = (IInvocationOperation)operation.Operation;
                    var owner = operation.ContainingSymbol;
                    for (var parent = invocation.Parent; parent != null; parent = parent.Parent)
                    {
                        if (parent is ILocalFunctionOperation local)
                        {
                            owner = local.Symbol;
                            break;
                        }
                        if (parent is IAnonymousFunctionOperation lambda)
                        {
                            owner = lambda.Symbol;
                            break;
                        }
                    }
                    if (owner is IMethodSymbol method)
                    {
                        calls.GetOrAdd(method.OriginalDefinition,
                            _ => new ConcurrentDictionary<ISymbol, byte>(SymbolEqualityComparer.Default))
                            .TryAdd(invocation.TargetMethod.OriginalDefinition, 0);
                    }
                    invocations.Add((invocation, owner));
                }, OperationKind.Invocation);
                start.RegisterCompilationEndAction(end =>
                {
                    foreach (var controller in controllers)
                        AnalyzeController(end, controller, calls);
                    foreach (var (invocation, owner) in invocations)
                        AnalyzeInvocation(end, invocation, owner, calls);
                });
            });
        }

        private static void AnalyzeController(
            CompilationAnalysisContext context, INamedTypeSymbol controller,
            ConcurrentDictionary<ISymbol, ConcurrentDictionary<ISymbol, byte>> calls)
        {
            var controllerNamespace = controller.ContainingNamespace.ToDisplayString();
            var cmdletNamespace = controllerNamespace.Replace(".Controllers.", ".Cmdlets.");
            if (controllerNamespace.EndsWith(".Controllers"))
            {
                cmdletNamespace = controllerNamespace.Substring(
                    0, controllerNamespace.Length - ".Controllers".Length) + ".Cmdlets";
            }
            var candidateNamespaces = new[] { controllerNamespace, cmdletNamespace }.Distinct();
            var cmdlet = candidateNamespaces
                .SelectMany(namespaceName => GetTypesInNamespace(
                    context.Compilation.GlobalNamespace, namespaceName))
                .FirstOrDefault(type =>
                {
                    var typeNamespace = type.ContainingNamespace.ToDisplayString();
                    if (typeNamespace != controllerNamespace && typeNamespace != cmdletNamespace) return false;

                    var attribute = GetCmdletAttribute(type);
                    if (attribute == null) return false;
                    var customName = attribute.NamedArguments
                        .FirstOrDefault(pair => pair.Key == "CustomControllerName").Value.Value as string;
                    var expectedControllerName = string.IsNullOrEmpty(customName)
                        ? type.Name + "Controller"
                        : customName + "Controller";
                    return controller.Name == expectedControllerName;
                });
            if (cmdlet == null) return;

            var run = FindMethod(controller, "Run");
            if (run == null || run.DeclaringSyntaxReferences.Length == 0) return;

            var supportsConfirmation = CmdletInfo.SetSupportsShouldProcess(cmdlet);
            var callsShouldProcess = CallsShouldProcess(
                run, controller, calls, new HashSet<ISymbol>(SymbolEqualityComparer.Default),
                context.CancellationToken);
            if (supportsConfirmation == callsShouldProcess) return;

            var location = controller.Locations.FirstOrDefault(item => item.IsInSource);
            if (location == null) return;
            context.ReportDiagnostic(Diagnostic.Create(
                supportsConfirmation
                    ? DiagnosticDescriptors.ControllerMustCallShouldProcess
                    : DiagnosticDescriptors.ConfirmationSupportMustBeEnabled,
                location, controller.Name));
        }

        private static IEnumerable<INamedTypeSymbol> GetTypesInNamespace(
            INamespaceSymbol root, string namespaceName)
        {
            var current = root;
            if (!string.IsNullOrEmpty(namespaceName))
            {
                foreach (var segment in namespaceName.Split('.'))
                {
                    current = current.GetNamespaceMembers().FirstOrDefault(item => item.Name == segment);
                    if (current == null) yield break;
                }
            }
            foreach (var type in current.GetTypeMembers()) yield return type;
        }

        private static AttributeData GetCmdletAttribute(INamedTypeSymbol type)
            => type.GetAttributes().FirstOrDefault(attribute =>
                attribute.AttributeClass?.ToDisplayString() == "TfsCmdlets.Cmdlets.TfsCmdletAttribute");

        private static IMethodSymbol FindMethod(INamedTypeSymbol type, string name)
        {
            for (var current = type; current != null; current = current.BaseType)
            {
                var method = current.GetMembers(name).OfType<IMethodSymbol>()
                    .FirstOrDefault(item => item.Parameters.Length == 0 && !item.IsAbstract);
                if (method != null) return method;
            }
            return null;
        }

        private static bool CallsShouldProcess(
            IMethodSymbol method, INamedTypeSymbol controller,
            ConcurrentDictionary<ISymbol, ConcurrentDictionary<ISymbol, byte>> calls,
            HashSet<ISymbol> visited, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            method = method.OriginalDefinition;
            if (!visited.Add(method)) return false;

            if (!calls.TryGetValue(method, out var targets)) return false;
            foreach (var symbol in targets.Keys)
            {
                var target = (IMethodSymbol)symbol;
                if (IsConfirmation(target, "ShouldProcess")) return true;
                if (IsControllerMethod(target, controller) &&
                    CallsShouldProcess(target, controller, calls, visited, cancellationToken)) return true;
            }
            return false;
        }

        private static bool IsControllerMethod(IMethodSymbol method, INamedTypeSymbol controller)
        {
            for (var current = controller; current != null; current = current.BaseType)
            {
                if (SymbolEqualityComparer.Default.Equals(
                    method.ContainingType.OriginalDefinition, current.OriginalDefinition)) return true;
            }
            return false;
        }

        private static bool IsConfirmation(IMethodSymbol method, string name)
        {
            if (method.Name != name || method.ReturnType.SpecialType != SpecialType.System_Boolean) return false;
            var type = method.ContainingType;
            if (type.ToDisplayString() == "TfsCmdlets.Services.IPowerShellService" ||
                type.AllInterfaces.Any(item => item.ToDisplayString() == "TfsCmdlets.Services.IPowerShellService"))
                return true;
            for (var current = type; current != null; current = current.BaseType)
            {
                if (current.ToDisplayString() == "System.Management.Automation.PSCmdlet") return true;
            }
            return false;
        }

        private static void AnalyzeInvocation(
            CompilationAnalysisContext context,
            IInvocationOperation invocation,
            ISymbol owner,
            ConcurrentDictionary<ISymbol, ConcurrentDictionary<ISymbol, byte>> calls)
        {
            var confirmationName = IsConfirmation(invocation.TargetMethod, "ShouldProcess")
                ? "ShouldProcess"
                : IsConfirmation(invocation.TargetMethod, "ShouldContinue")
                    ? "ShouldContinue"
                    : null;

            if (confirmationName == null &&
                invocation.TargetMethod.ReturnType.SpecialType == SpecialType.System_Boolean &&
                owner is IMethodSymbol ownerMethod &&
                IsControllerMethod(invocation.TargetMethod, ownerMethod.ContainingType))
            {
                if (CallsConfirmation(invocation.TargetMethod, ownerMethod.ContainingType,
                    calls, new HashSet<ISymbol>(SymbolEqualityComparer.Default),
                    context.CancellationToken, "ShouldProcess"))
                    confirmationName = "ShouldProcess";
                else if (CallsConfirmation(invocation.TargetMethod, ownerMethod.ContainingType,
                    calls, new HashSet<ISymbol>(SymbolEqualityComparer.Default),
                    context.CancellationToken, "ShouldContinue"))
                    confirmationName = "ShouldContinue";
            }

            if (confirmationName == null) return;

            IOperation result = invocation;
            while (true)
            {
                if (result.Parent is IConversionOperation || result.Parent is IParenthesizedOperation)
                {
                    result = result.Parent;
                    continue;
                }
                if (result.Parent is IConditionalAccessOperation conditionalAccess &&
                    conditionalAccess.WhenNotNull == result)
                {
                    result = conditionalAccess;
                    continue;
                }
                break;
            }

            if (result.Parent is IExpressionStatementOperation ||
                (result.Parent is ISimpleAssignmentOperation assignment && assignment.Target is IDiscardOperation))
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.ConfirmationResultMustBeUsed,
                    invocation.Syntax.GetLocation(), confirmationName));
            }
        }

        private static bool CallsConfirmation(
            IMethodSymbol method,
            INamedTypeSymbol controller,
            ConcurrentDictionary<ISymbol, ConcurrentDictionary<ISymbol, byte>> calls,
            HashSet<ISymbol> visited,
            CancellationToken cancellationToken,
            string confirmationName)
        {
            cancellationToken.ThrowIfCancellationRequested();
            method = method.OriginalDefinition;
            if (!visited.Add(method) || !calls.TryGetValue(method, out var targets)) return false;

            foreach (var symbol in targets.Keys)
            {
                var target = (IMethodSymbol)symbol;
                if (IsConfirmation(target, confirmationName)) return true;
                if (IsControllerMethod(target, controller) &&
                    CallsConfirmation(target, controller, calls, visited,
                        cancellationToken, confirmationName)) return true;
            }
            return false;
        }
    }
}
