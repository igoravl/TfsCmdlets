using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using TfsCmdlets.SourceGenerators.Analyzers;

namespace TfsCmdlets.SourceGenerators.UnitTests.Analyzers;

public class ConfirmationSafetyTests
{
    private const string Infrastructure = """
        namespace TfsCmdlets.Cmdlets
        {
            public sealed class TfsCmdletAttribute : System.Attribute
            {
                public bool SupportsShouldProcess { get; set; }
                public string CustomControllerName { get; set; }
            }
        }
        namespace TfsCmdlets.Services
        {
            public interface IPowerShellService
            {
                bool ShouldProcess(string target, string action);
                bool ShouldContinue(string query);
            }
        }
        namespace System.Management.Automation
        {
            public abstract class PSCmdlet
            {
                protected bool ShouldProcess(string target, string action) => true;
            }
        }
        namespace Example
        {
            abstract class ControllerBase
            {
                protected TfsCmdlets.Services.IPowerShellService PowerShell;
                protected abstract void Run();
            }
        }
        """;

    [Theory]
    [InlineData("New")]
    [InlineData("Set")]
    [InlineData("Remove")]
    [InlineData("Start")]
    [InlineData("Invoke")]
    public async Task ReportsMissingGuardForGeneratorDefaults(string verb)
    {
        var diagnostics = await AnalyzeAsync(Source(verb, "", "Write();"));
        Assert.Equal("TFS006", Assert.Single(diagnostics).Id);
    }

    [Theory]
    [InlineData("Get")]
    [InlineData("Test")]
    [InlineData("Search")]
    [InlineData("Connect")]
    [InlineData("Disconnect")]
    public async Task RespectsGeneratorReadOnlyDefaults(string verb)
        => Assert.Empty(await AnalyzeAsync(Source(verb, "", "Write();")));

    [Fact]
    public async Task ExplicitSupportOverridesReadOnlyDefault()
        => Assert.Equal("TFS006", Assert.Single(await AnalyzeAsync(
            Source("Get", "SupportsShouldProcess = true", "Write();"))).Id);

    [Fact]
    public async Task ExplicitlyDisabledSupportWithoutGuardIsNotDiagnosed()
        => Assert.Empty(await AnalyzeAsync(Source("New", "SupportsShouldProcess = false", "Write();")));

    [Fact]
    public async Task ReportsDisabledSupportWhenControllerUsesGuard()
        => Assert.Equal("TFS008", Assert.Single(await AnalyzeAsync(Source(
            "New", "SupportsShouldProcess = false",
            """if (!PowerShell.ShouldProcess("item", "write")) return; Write();"""))).Id);

    [Fact]
    public async Task ReportsGuardWithReadOnlyDefault()
        => Assert.Equal("TFS008", Assert.Single(await AnalyzeAsync(Source(
            "Get", "", """if (!PowerShell.ShouldProcess("item", "write")) return; Write();"""))).Id);

    [Fact]
    public async Task AcceptsGuardInPartialHelper()
    {
        var helper = """
            namespace Example
            {
                partial class NewItemController
                {
                    private bool Confirm() => PowerShell.ShouldProcess("item", "write");
                }
            }
            """;
        Assert.Empty(await AnalyzeAsync(Source("New", "", "if (!Confirm()) return; Write();"), helper));
    }

    [Fact]
    public async Task DoesNotAcceptUncalledHelperAsGuard()
    {
        var source = Source("New", "", "Write();") + """
            namespace Example
            {
                partial class NewItemController
                {
                    private bool Confirm() => PowerShell.ShouldProcess("item", "write");
                }
            }
            """;
        Assert.Equal("TFS006", Assert.Single(await AnalyzeAsync(source)).Id);
    }

    [Fact]
    public async Task AcceptsCalledLocalFunction()
        => Assert.Empty(await AnalyzeAsync(Source("New", "", """
            bool Confirm() => PowerShell.ShouldProcess("item", "write");
            if (!Confirm()) return;
            Write();
            """)));

    [Fact]
    public async Task DoesNotAcceptUnusedLocalFunction()
        => Assert.Equal("TFS006", Assert.Single(await AnalyzeAsync(Source("New", "", """
            bool Confirm() => PowerShell.ShouldProcess("item", "write");
            Write();
            """))).Id);

    [Fact]
    public async Task DoesNotAcceptUnusedLambda()
        => Assert.Equal("TFS006", Assert.Single(await AnalyzeAsync(Source("New", "", """
            System.Func<bool> confirm = () => PowerShell.ShouldProcess("item", "write");
            Write();
            """))).Id);

    [Fact]
    public async Task HandlesRecursiveHelpersWithoutHanging()
    {
        var source = Source("New", "", "Again();") + """
            namespace Example
            {
                partial class NewItemController
                {
                    private void Again() => Again();
                }
            }
            """;
        Assert.Equal("TFS006", Assert.Single(await AnalyzeAsync(source)).Id);
    }

    [Theory]
    [InlineData("ShouldProcess", """("item", "write")""")]
    [InlineData("ShouldContinue", """("continue?")""")]
    public async Task ReportsDiscardedConfirmationResult(string method, string arguments)
    {
        // The read-only cmdlet keeps this test focused on result handling, not missing guards.
        var diagnostics = await AnalyzeAsync(Source("Get", "SupportsShouldProcess = true",
            $"""if (!PowerShell.ShouldProcess("item", "write")) return; PowerShell.{method}{arguments};"""));
        Assert.Equal("TFS007", Assert.Single(diagnostics).Id);
    }

    [Fact]
    public async Task ReportsExplicitDiscard()
        => Assert.Equal("TFS007", Assert.Single(await AnalyzeAsync(Source("New", "",
            """_ = PowerShell.ShouldProcess("item", "write"); Write();"""))).Id);

    [Fact]
    public async Task ReportsDiscardedConfirmationHelperResults()
    {
        var diagnostics = await AnalyzeAsync(Source("New", "", """
            bool Confirm() => PowerShell.ShouldProcess("item", "write");
            bool Continue() => PowerShell.ShouldContinue("continue?");
            Confirm();
            Continue();
            Write();
            """));

        Assert.Equal(2, diagnostics.Length);
        Assert.All(diagnostics, diagnostic => Assert.Equal("TFS007", diagnostic.Id));
    }

    [Fact]
    public async Task IgnoresUnrelatedMethodNamedShouldProcess()
    {
        var source = Source("New", "", """if (!Other.ShouldProcess("item", "write")) return; Write();""") + """
            namespace Example
            {
                static class Other
                {
                    public static bool ShouldProcess(string target, string action) => true;
                }
            }
            """;
        Assert.Equal("TFS006", Assert.Single(await AnalyzeAsync(source)).Id);
    }

    [Fact]
    public async Task RecognizesCustomControllerName()
    {
        var source = Source("New", """CustomControllerName = "CustomController" """, "Write();")
            .Replace("class NewItemController", "class CustomController");
        Assert.Equal("TFS006", Assert.Single(await AnalyzeAsync(source)).Id);
    }

    [Fact]
    public async Task RecognizesInheritedGuard()
    {
        var source = Source("New", "", "if (!Confirm()) return; Write();")
            .Replace(": ControllerBase", ": GuardedControllerBase") + """
            namespace Example
            {
                abstract class GuardedControllerBase : ControllerBase
                {
                    protected bool Confirm() => PowerShell.ShouldProcess("item", "write");
                }
            }
            """;
        Assert.Empty(await AnalyzeAsync(source));
    }

    [Fact]
    public async Task RecognizesPowerShellCmdletConfirmation()
    {
        const string source = """
            namespace Example
            {
                class DirectCmdlet : System.Management.Automation.PSCmdlet
                {
                    void Run() { ShouldProcess("item", "write"); }
                }
            }
            """;
        Assert.Equal("TFS007", Assert.Single(await AnalyzeAsync(source)).Id);
    }

    [Fact]
    public async Task IgnoresGeneratedCode()
        => Assert.Empty(await AnalyzeAsync("// <auto-generated/>\n" + Source("New", "", """
            PowerShell.ShouldProcess("item", "write");
            """)));

    [Fact]
    public async Task AcceptsConfirmationUsedInCompoundConditions()
        => Assert.Empty(await AnalyzeAsync(Source("Remove", "", """
            bool force = false;
            if (PowerShell.ShouldProcess("item", "delete") &&
                (force || PowerShell.ShouldContinue("continue?"))) Write();
            """)));

    private static string Source(string verb, string options, string body) => $$"""
        namespace Example
        {
            [TfsCmdlets.Cmdlets.TfsCmdlet({{options}})]
            partial class {{verb}}Item { }
            partial class {{verb}}ItemController : ControllerBase
            {
                protected override void Run() { {{body}} }
                private void Write() { }
            }
        }
        """;

    private static async Task<ImmutableArray<Diagnostic>> AnalyzeAsync(params string[] sources)
    {
        var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
            .Split(Path.PathSeparator).Select(path => MetadataReference.CreateFromFile(path));
        var compilation = CSharpCompilation.Create("AnalyzerTest",
            new[] { CSharpSyntaxTree.ParseText(Infrastructure) }
                .Concat(sources.Select(source => CSharpSyntaxTree.ParseText(source))),
            references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        Assert.Empty(compilation.GetDiagnostics().Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error));
        return await compilation.WithAnalyzers(
            ImmutableArray.Create<DiagnosticAnalyzer>(new ConfirmationSafetyAnalyzer())).GetAnalyzerDiagnosticsAsync();
    }
}
