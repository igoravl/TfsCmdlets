using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    public class ControllerGenerator : IGenerator
    {
        internal static INamedTypeSymbol ControllerBase { get; set; }
        internal static INamedTypeSymbol ControllerBaseT { get; set; }
        private IDictionary<string, GeneratorState> GeneratedStates { get; set; }
        private static readonly ICollection<string> _scopeProperties = new[] { "Server", "Collection", "Project", "Team" };
        private static readonly List<(Predicate<ControllerInfo>, Func<ControllerInfo, IEnumerable<GeneratedProperty>>)> _generators =
            new List<(Predicate<ControllerInfo>, Func<ControllerInfo, IEnumerable<GeneratedProperty>>)>()
            {
                // Get "input" property
                ((controller) => controller.Verb.Equals("Get") && controller.DeclaredProperties.Count > 0, GenerateGetInputProperty),

                // Cmdlet declared parameters
                ((controller) => controller.DeclaredProperties.Count > 0, GenerateDeclaredParameters),

                // Cmdlet implicit (source-gen'ed) parameters
                ((controller) => controller.ImplicitProperties.Count > 0, GenerateImplicitParameters),

                // Scope parameters
                ((controller) => (int)controller.CmdletInfo.Scope >= (int)CmdletScope.Team, ci => GenerateScopeProperty(ci, CmdletScope.Team, "WebApiTeam")),
                ((controller) => (int)controller.CmdletInfo.Scope >= (int)CmdletScope.Project, ci => GenerateScopeProperty(ci, CmdletScope.Project, "WebApiTeamProject")),
                ((controller) => (int)controller.CmdletInfo.Scope >= (int)CmdletScope.Collection, ci => GenerateScopeProperty(ci, CmdletScope.Collection, "Models.Connection")),
                ((controller) => (int)controller.CmdletInfo.Scope >= (int)CmdletScope.Server, ci => GenerateScopeProperty(ci, CmdletScope.Server, "Models.Connection")), 

                // ParameterSetName
                ((controller) => true, GenerateParameterSetProperty),

                // 'Items' property
                ((controller) => !controller.Verb.Equals("Get") && controller.DeclaredProperties.Count > 0 &&
                    controller.DeclaredProperties.Values.First().Type.Equals("object"), GenerateItemsProperty),

                // DataType
                ((controller) => !string.IsNullOrEmpty(controller.GenericArg), GenerateDataType),
            };

        public ControllerGenerator(IDictionary<string, GeneratorState> generatedStates)
        {
            GeneratedStates = generatedStates;
        }

        public void Initialize(GeneratorExecutionContext context)
        {
            ControllerBase = context.Compilation.GetTypeByMetadataName("TfsCmdlets.Controllers.ControllerBase");
            ControllerBaseT = context.Compilation.GetTypeByMetadataName("TfsCmdlets.Controllers.ControllerBase`1");
        }

        public GeneratorState ProcessType(GeneratorExecutionContext context, INamedTypeSymbol type)
        {
            var controller = new ControllerInfo(type, GeneratedStates, context);

            foreach (var (condition, generator) in _generators)
            {
                if (!condition(controller)) continue;

                foreach (var prop in generator(controller))
                {
                    if (controller.GeneratedProperties.ContainsKey(prop.Name))
                    {
                        Logger.Log($"[ERROR] Property '{prop.Name}' already exists in '{controller.FullName}'");
                        continue;
                    }

                    //Logger.Log($"- Adding {prop.Name}");

                    controller.GeneratedProperties.Add(prop.Name, prop);
                }
            }

            return controller;
        }

        public string Generate(GeneratorState state)
        {
            var controller = (ControllerInfo)state;
            var props = new StringBuilder();
            var cacheProps = new StringBuilder();

            foreach (var prop in controller.GeneratedProperties.Values)
            {
                props.Append(prop.ToString());

                if (prop.IsHidden || prop.Name.Equals("Items") ||
                    _scopeProperties.Contains(prop.Name) ||
                    (controller.Verb.Equals("Get") &&
                        controller.DeclaredProperties.Count > 0 &&
                        controller.DeclaredProperties.Values.First().Name.Equals(prop.Name))) continue;

                var type = prop.Type.ToString().EndsWith("SwitchParameter") ? "bool" : prop.Type.ToString();
                var initializer = string.IsNullOrEmpty(prop.DefaultValue) ? string.Empty : $", {prop.DefaultValue}";

                cacheProps.Append($"            // {prop.Name}\n");
                cacheProps.Append($"            Has_{prop.Name} = Parameters.HasParameter(\"{prop.Name}\");\n");
                cacheProps.Append($"            {prop.Name} = Parameters.Get<{type}>(\"{prop.Name}\"{initializer});\n\n");
            }

            return $@"/*
Generated by: [{this.GetType().FullName}]
Class name  : {controller.FullName}
Base class  : {controller.BaseClass}
*/
using System;
using System.CodeDom.Compiler;
using System.Composition;
using System.Management.Automation;
using TfsCmdlets.Controllers;
using TfsCmdlets.Services;

// ReSharper disable once CheckNamespace
namespace {controller.Namespace}
{{
    internal partial class {controller.Name}: {controller.BaseClassName}
    {{
{props}

        protected override void CacheParameters()
        {{
{cacheProps}
        }}

        [ImportingConstructor]
        public {controller.Name}({controller.CtorArgs})
            : base({controller.BaseCtorArgs})
        {{
{controller.ImportingConstructorBody}
        }}
    }}
}}
";
        }

        private static IEnumerable<GeneratedProperty> GenerateParameterSetProperty(ControllerInfo controller)
        {
            yield return new GeneratedProperty("ParameterSetName", "string", $@"        // ParameterSetName
        protected bool Has_ParameterSetName {{get;set;}}
        protected string ParameterSetName {{get;set;}}
");
        }

        private static IEnumerable<GeneratedProperty> GenerateDataType(ControllerInfo controller)
        {
            yield return new GeneratedProperty("DataType", controller.DataType.ToString(), true, $@"        // DataType
        public override Type DataType => typeof({controller.DataType});

");
        }

        private static IEnumerable<GeneratedProperty> GenerateItemsProperty(ControllerInfo controller)
        {
            yield return new GeneratedProperty("Items", "object", !controller.IsGeneric ?
                $@"
        // Items
        protected IEnumerable Items => Data.Invoke(""Get"", ""{controller.Noun}"");

" :
                $@"
        // Items
        protected IEnumerable{controller.GenericArg} Items => {controller.DeclaredProperties.Values.First().Name} switch {{
            {controller.DataType} item => new[] {{ item }},
            IEnumerable{controller.GenericArg} items => items,
            _ => Data.GetItems{controller.GenericArg}()
        }};

");
        }

        private static IEnumerable<GeneratedProperty> GenerateGetInputProperty(ControllerInfo controller)
        {
            var prop = controller.DeclaredProperties.Values.First();

            var initializer = string.IsNullOrEmpty(prop.DefaultValue) ? string.Empty : $", {prop.DefaultValue}";

            yield return new GeneratedProperty(prop.Name, "IEnumerable", $@"        // {prop.Name}
        protected bool Has_{prop.Name} {{get;set;}}
        protected IEnumerable {prop.Name}
        {{
            get
            {{
                var paramValue = Parameters.Get<object>(""{prop.Name}""{initializer});
                if(paramValue is ICollection col) return col;
                return new[] {{ paramValue }};
            }}
        }}

");
        }

        private static IEnumerable<GeneratedProperty> GenerateDeclaredParameters(ControllerInfo controller)
        {
            foreach (var prop in controller.DeclaredProperties.Values.Skip(controller.Verb.Equals("Get") ? 1 : 0))
            {
                var type = prop.Type.EndsWith("SwitchParameter") ? "bool" : prop.Type;

                yield return new GeneratedProperty(prop.Name, prop.Type.ToString(), $@"        // {prop.Name}
        protected bool Has_{prop.Name} {{ get; set; }}
        protected {type} {prop.Name} {{ get; set; }}

") { DefaultValue = prop.DefaultValue };
            }
        }

        private static IEnumerable<GeneratedProperty> GenerateImplicitParameters(ControllerInfo controller)
        {
            foreach (var prop in controller.CmdletInfo.GeneratedProperties.Values)
            {
                var type = prop.Type.ToString().EndsWith("SwitchParameter") ? "bool" : prop.Type.ToString();

                if (_scopeProperties.Any(s => prop.Name.Equals(s))) continue;

                if (prop.IsHidden) continue;

                yield return new GeneratedProperty(prop.Name, type, $@"        // {prop.Name}
        protected bool Has_{prop.Name} {{get;set;}} // => Parameters.HasParameter(""{prop.Name}"");
        protected {type} {prop.Name} {{get;set;}} // => _{prop.Name}; // Parameters.Get<{type}>(""{prop.Name}"");

");
            }
        }


        private static IEnumerable<GeneratedProperty> GenerateScopeProperty(ControllerInfo controller, CmdletScope scope, string scopeType)
        {
            yield return new GeneratedProperty(scope.ToString(), "object", $@"        // {scope}
        protected {scopeType} {scope} => Data.Get{scope}();

");
        }
    }
}