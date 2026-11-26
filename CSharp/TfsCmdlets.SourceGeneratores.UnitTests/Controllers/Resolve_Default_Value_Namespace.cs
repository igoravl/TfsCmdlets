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
        public void Fully_Qualifies_Enum_Type_And_Default_Value()
        {
            const string source = """
                using System;
                using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
                using TfsCmdlets;
                using System.Composition;

                namespace TfsCmdlets
                {
                    public enum CmdletScope { None, Collection }

                    [AttributeUsage(AttributeTargets.Class)]
                    public sealed class TfsCmdletAttribute : Attribute
                    {
                        public TfsCmdletAttribute(CmdletScope scope) { }
                        public Type OutputType { get; set; }
                    }

                    [AttributeUsage(AttributeTargets.Class)]
                    public sealed class CmdletControllerAttribute : Attribute
                    {
                        public CmdletControllerAttribute(Type dataType) { }
                    }

                    [AttributeUsage(AttributeTargets.Property)]
                    public sealed class ParameterAttribute : Attribute { }

                    public interface IParameterManager
                    {
                        bool HasParameter(string name);
                        T Get<T>(string name, T defaultValue = default);
                    }

                    public interface IDataManager
                    {
                        Models.Connection GetServer();
                        Models.Connection GetCollection();
                    }
                }

                namespace TfsCmdlets.Controllers
                {
                    public class ControllerBase
                    {
                        protected TfsCmdlets.IDataManager Data { get; }
                        protected TfsCmdlets.IParameterManager Parameters { get; }
                        public virtual Type DataType => GetType();
                        protected virtual void CacheParameters() { }
                    }
                }

                namespace System.Composition
                {
                    [AttributeUsage(AttributeTargets.Constructor)]
                    public sealed class ImportingConstructorAttribute : Attribute { }
                }

                namespace TfsCmdlets.Models
                {
                    public sealed class Connection { }
                }

                namespace Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models
                {
                    public enum FieldType { String }
                    public sealed class WorkItemField { }
                }

                namespace TfsCmdlets.Cmdlets.WorkItem.Field
                {
                    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WorkItemField))]
                    public partial class NewWorkItemField
                    {
                        [Parameter]
                        public FieldType Type { get; set; } = FieldType.String;
                    }
                }

                namespace TfsCmdlets.Controllers.WorkItem.Field
                {
                    [CmdletController(typeof(WorkItemField))]
                    internal partial class NewWorkItemFieldController { }
                }
                """;

            var trustedAssemblies = AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") as string
                ?? throw new InvalidOperationException("The runtime did not provide trusted platform assemblies.");
            var references = trustedAssemblies
                .Split(Path.PathSeparator)
                .Select(path => MetadataReference.CreateFromFile(path));
            var compilation = CSharpCompilation.Create(
                "GeneratorTests",
                new[] { CSharpSyntaxTree.ParseText(source) },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            GeneratorDriver driver = CSharpGeneratorDriver.Create(new ControllerGenerator());
            driver = driver.RunGeneratorsAndUpdateCompilation(
                compilation,
                out var outputCompilation,
                out _);

            var generated = Assert.Single(driver.GetRunResult().GeneratedTrees).GetText().ToString();

            Assert.Contains("Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType Type", generated);
            Assert.Contains("Parameters.Get<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType>(\"Type\", FieldType.String)", generated);
            var errors = outputCompilation.GetDiagnostics()
                .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
                .ToArray();
            Assert.True(errors.Length == 0, string.Join(Environment.NewLine, errors.Select(error => error.ToString())));
        }
    }
}
