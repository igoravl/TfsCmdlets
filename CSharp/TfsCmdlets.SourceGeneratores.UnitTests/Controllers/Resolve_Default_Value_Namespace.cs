using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsCmdlets.SourceGenerators.Generators.Controllers;
using Xunit;
using VerifyCS = TfsCmdlets.SourceGenerators.UnitTests.CSharpSourceGeneratorVerifier<TfsCmdlets.SourceGenerators.Generators.Controllers.ControllerGenerator>;

namespace TfsCmdlets.SourceGenerators.UnitTests.Controllers
{
    public class Resolve_Default_Value_Namespace
    {
        [Fact]
        public async Task Can_Resolve_Enum_Namespaces()
        {
            var code = """
                using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
                using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

                namespace TfsCmdlets.Cmdlets.WorkItem.Field
                {
                    /// <summary>
                    /// Gets information from one or more process templates.
                    /// </summary>
                    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WorkItemField))]
                    partial class NewWorkItemField
                    {
                        /// <summary>
                        /// Specifies the type of the field.
                        /// </summary>
                        [Parameter]
                        public FieldType Type { get; set; } = FieldType.String;
                    }
                }
                """;

            var generated = """
                namespace TfsCmdlets.Cmdlets.WorkItem.Field
                {
                    internal partial class NewWorkItemFieldController: ControllerBase
                    {
                        // Type
                        protected bool Has_Type { get; set; }
                        protected Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType Type { get; set; }

                        // Passthru
                        protected bool Has_Passthru {get;set;} // => Parameters.HasParameter("Passthru");
                        protected bool Passthru {get;set;} // => _Passthru; // Parameters.Get<bool>("Passthru");

                        // Collection
                        protected bool Has_Collection => Parameters.HasParameter("Collection");
                        protected Models.Connection Collection => Data.GetCollection();

                        // Server
                        protected bool Has_Server => Parameters.HasParameter("Server");
                        protected Models.Connection Server => Data.GetServer();

                        // ParameterSetName
                        protected bool Has_ParameterSetName {get;set;}
                        protected string ParameterSetName {get;set;}


                        protected override void CacheParameters()
                        {
                            // Field
                            Has_Field = Parameters.HasParameter("Field");
                            Field = Parameters.Get<string>("Field");

                            // ReferenceName
                            Has_ReferenceName = Parameters.HasParameter("ReferenceName");
                            ReferenceName = Parameters.Get<string>("ReferenceName");

                            // Description
                            Has_Description = Parameters.HasParameter("Description");
                            Description = Parameters.Get<string>("Description");

                            // Type
                            Has_Type = Parameters.HasParameter("Type");
                            Type = Parameters.Get<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType>("Type", FieldType.String);
                """;

            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = { code },
                    GeneratedSources =
                    {
                        (typeof(ControllerGenerator), "NewWorkItemFieldController.cs", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                    },
                },
            }.RunAsync();
        }
    }
}
