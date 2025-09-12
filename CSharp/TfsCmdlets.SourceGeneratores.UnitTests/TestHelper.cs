using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace TfsCmdlets.SourceGenerators.UnitTests
{
    internal static class TestHelper
    {
        private static readonly List<PortableExecutableReference> _References = RoslynReferenceHelper.LoadReferences();

        internal static Task Verify<T>(string testName, string source)
            where T : IIncrementalGenerator, new()
        {
            // Parse the provided string into a C# syntax tree
            var syntaxTree = CSharpSyntaxTree.ParseText(source);

            // Create a Roslyn compilation for the syntax tree.
            var compilation = CSharpCompilation.Create(
                assemblyName: "TfsCmdlets_SrcGen_Tests",
                syntaxTrees: [syntaxTree],
                references: _References);

            // Create an instance of the source generator
            var generator = new T();

            // The GeneratorDriver is used to run our generator against a compilation
            var driver = CSharpGeneratorDriver
                .Create(generator)
                .RunGenerators(compilation);

            var settings = new VerifySettings();
            settings.UseUniqueDirectory();
            //settings.UseSplitModeForUniqueDirectory();
            settings.UseDirectory($"_Verify/{generator.GetType().Name}");
            settings.UseFileName(testName);

            // Use verify to snapshot test the source generator output!
            return Verifier.Verify(driver, settings);
        }
    }
}