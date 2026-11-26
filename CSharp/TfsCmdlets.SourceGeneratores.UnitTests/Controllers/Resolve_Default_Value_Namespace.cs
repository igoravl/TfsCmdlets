using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TfsCmdlets.SourceGenerators.Generators.Controllers;
using Xunit;

namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers
{
    public class Resolve_Default_Value_Namespace
    {
        [Fact]
        public void Can_Resolve_Enum_Namespaces()
        {
            const string source = """
                using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

                namespace TfsCmdlets.Cmdlets
                {
                    public enum CmdletScope { Collection }
                    [System.AttributeUsage(System.AttributeTargets.Class)]
                    public sealed class TfsCmdletAttribute : System.Attribute
                    {
                        public TfsCmdletAttribute(CmdletScope scope) { }
                        public System.Type OutputType { get; set; }
                    }
                    [System.AttributeUsage(System.AttributeTargets.Property)]
                    public sealed class ParameterAttribute : System.Attribute { }
                    [System.AttributeUsage(System.AttributeTargets.Class)]
                    public sealed class CmdletControllerAttribute : System.Attribute
                    {
                        public CmdletControllerAttribute(System.Type dataType) { }
                    }
                }

                namespace TfsCmdlets.Controllers
                {
                    public class ControllerBase
                    {
                        public ControllerBase() { }
                        protected virtual void CacheParameters() { }
                    }
                }

                namespace Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models
                {
                    public enum FieldType { String }
                    public sealed class WorkItemField { }
                }

                namespace TfsCmdlets.Cmdlets.WorkItem.Field
                {
                    [TfsCmdlets.Cmdlets.TfsCmdlet(
                        TfsCmdlets.Cmdlets.CmdletScope.Collection,
                        OutputType = typeof(WorkItemField))]
                    public partial class NewWorkItemField
                    {
                        [TfsCmdlets.Cmdlets.Parameter]
                        public FieldType Type { get; set; } = FieldType.String;
                    }
                }

                namespace TfsCmdlets.Controllers.WorkItem.Field
                {
                    [TfsCmdlets.Cmdlets.CmdletController(
                        typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField))]
                    public partial class NewWorkItemFieldController
                        : TfsCmdlets.Controllers.ControllerBase
                    {
                    }
                }
                """;

            var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
                .Split(Path.PathSeparator)
                .Select(path => MetadataReference.CreateFromFile(path));
            var compilation = CSharpCompilation.Create(
                "GeneratorTest",
                new[] { CSharpSyntaxTree.ParseText(source) },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            Assert.Empty(compilation.GetDiagnostics()
                .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error));

            GeneratorDriver driver = CSharpGeneratorDriver.Create(new ControllerGenerator());
            driver = driver.RunGenerators(compilation);

            var generated = Assert.Single(driver.GetRunResult().GeneratedTrees).GetText().ToString();
            Assert.Contains(
                "protected Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType Type",
                generated);
            Assert.Contains(
                "Parameters.Get<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType>(\"Type\", FieldType.String)",
                generated);
        }
    }
}
