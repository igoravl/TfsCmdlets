using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TfsCmdlets.SourceGenerators.UnitTests
{
    internal static class TestHelper
    {
        private static readonly List<PortableExecutableReference> _References = RoslynReferenceHelper.LoadReferences();
        private static readonly Lazy<string[]> _GlobalUsings = new(ExtractGlobalUsings);

        private static string[] ExtractGlobalUsings()
        {
            var globalUsings = new List<string>();

            try
            {
                var projectRoot = FindProjectRoot();
                
                var globalUsingsFile = Path.Combine(projectRoot, "TfsCmdlets.Shared/GlobalUsings.cs");
                if (File.Exists(globalUsingsFile))
                {
                    globalUsings.AddRange(ParseGlobalUsingsFromFile(globalUsingsFile));
                }

                var csprojFiles = Directory.GetFiles(projectRoot, "*.csproj");
                foreach (var csprojFile in csprojFiles)
                {
                    globalUsings.AddRange(ParseGlobalUsingsFromCsproj(csprojFile));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting global usings: {ex.Message}");
            }

            if (!globalUsings.Any())
            {
                globalUsings.AddRange(new[]
                {
                    "System",
                    "System.Collections.Generic",
                    "System.Linq",
                    "System.Threading.Tasks"
                });
            }

            return globalUsings.Distinct().ToArray();
        }

        public static string FindProjectRoot()
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (directory != null)
            {
                if (directory.GetFiles("*.sln").Any())
                {
                    return directory.FullName;
                }
                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException("Could not find the project root directory.");
        }

        private static IEnumerable<string> ParseGlobalUsingsFromFile(string filePath)
        {
            var content = File.ReadAllText(filePath);
            var regex = new Regex(@"global\s+using\s+([^;]+);", RegexOptions.Multiline);
            var matches = regex.Matches(content);

            return matches.Cast<Match>()
                         .Select(m => m.Groups[1].Value.Trim())
                         .Where(u => !string.IsNullOrWhiteSpace(u));
        }

        private static IEnumerable<string> ParseGlobalUsingsFromCsproj(string csprojPath)
        {
            try
            {
                var content = File.ReadAllText(csprojPath);
                var regex = new Regex(@"<Using\s+Include\s*=\s*[""']([^""']+)[""']\s*/>", RegexOptions.IgnoreCase);
                var matches = regex.Matches(content);

                return matches.Cast<Match>()
                             .Select(m => m.Groups[1].Value.Trim())
                             .Where(u => !string.IsNullOrWhiteSpace(u));
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        internal static Task VerifyFiles<T>(string testName, IEnumerable<string> testFiles)
            where T : IIncrementalGenerator, new()
        {
            var files = testFiles.ToList();

            if (files.Count == 0)
            {
                throw new ArgumentException("No test files provided.", nameof(testFiles));
            }

            var root = FindProjectRoot();
            var mainFile = Path.Combine(root, files[0]);
            var additionalFiles = files.Skip(1).Select(f => Path.Combine(root, f)).ToList();

            return Verify<T>(testName, File.ReadAllText(mainFile), additionalFiles);
        }

        internal static Task Verify<T>(string testName, string source, IEnumerable<string>? additionalFiles = null)
            where T : IIncrementalGenerator, new()
        {
            // Parse the provided string into a C# syntax tree
            var syntaxTrees = new List<SyntaxTree>();
            syntaxTrees.Add(CSharpSyntaxTree.ParseText(source));

            // Configure the compilation options, including global usings
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectRoot = FindProjectRoot();
            var globalUsingsFile = Path.Combine(projectRoot, "TfsCmdlets.Shared/GlobalUsings.cs");
            var globalUsings = File.ReadAllText(globalUsingsFile);
            syntaxTrees.Add(CSharpSyntaxTree.ParseText(globalUsings));

            // Include additional files if provided
            if (additionalFiles is not null)
            {
                foreach (var file in additionalFiles)
                {
                    var f = Path.Combine(projectRoot, file);
                    if (!File.Exists(f)) continue;
                    var fileContent = File.ReadAllText(f);
                    syntaxTrees.Add(CSharpSyntaxTree.ParseText(fileContent));
                }
            }

            // Create a Roslyn compilation for the syntax tree.
            var compilation = CSharpCompilation.Create(
                assemblyName: "TfsCmdlets_SrcGen_Tests",
                syntaxTrees: syntaxTrees,
                references: _References);

            // Create an instance of the source generator
            var generator = new T();

            // The GeneratorDriver is used to run our generator against a compilation
            var driver = CSharpGeneratorDriver
                .Create(generator)
                .RunGenerators(compilation);

            var settings = new VerifySettings();
            //settings.UseUniqueDirectory();
            //settings.UseSplitModeForUniqueDirectory();
            settings.UseDirectory($"_Verify/{generator.GetType().Name}");
            settings.UseFileName(testName);
            settings.ScrubEmptyLines();

            // Use verify to snapshot test the source generator output!
            return Verifier.Verify(driver, settings);
        }
    }
}