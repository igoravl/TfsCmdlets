using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using TfsCmdlets.SourceGenerators.Generators.Cmdlets;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    public class ControllerInfo : GeneratorState
    {
        public string Noun { get; }
        public CmdletInfo CmdletInfo { get; }
        public string GenericArg { get; }
        internal string Verb { get; }
        public INamedTypeSymbol DataType { get; }
        public INamedTypeSymbol BaseClass { get; }
        public string CmdletName { get; }
        public INamedTypeSymbol Cmdlet { get; }
        public string CtorArgs { get; }
        public string BaseCtorArgs { get; }
        public string ImportingConstructorBody { get; }
        public IDictionary<string, GeneratedProperty> DeclaredProperties { get; }
        public IDictionary<string, GeneratedProperty> ImplicitProperties { get; }
        public string BaseClassName => BaseClass.Name;
        public bool SkipGetProperty => CmdletInfo.SkipGetProperty;

        internal ControllerInfo(INamedTypeSymbol controller, GeneratorExecutionContext context, Logger logger)
            : base(controller, logger)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));

            var customBaseClass = controller.GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "CustomBaseClass");
            var cmdletName = controller.FullName().Replace(".Controllers.", ".Cmdlets.");

            var customCmdletClass = controller.GetAttributeNamedValue<string>("CmdletControllerAttribute", "CustomCmdletName");

            if (!string.IsNullOrEmpty(customCmdletClass)) cmdletName = cmdletName.Replace(controller.Name, $"{customCmdletClass}Controller");

            CmdletName = cmdletName.Substring(0, cmdletName.Length - "Controller".Length);
            Cmdlet = context.Compilation.GetTypeByMetadataName(CmdletName);

            if (Cmdlet == null) throw new ArgumentException($"Unable to find cmdlet class '{CmdletName}'");

            BaseClass = customBaseClass ?? context.Compilation.GetTypeByMetadataName("TfsCmdlets.Controllers.ControllerBase"); ;
            DataType = controller.GetAttributeConstructorValue<INamedTypeSymbol>("CmdletControllerAttribute"); ;
            GenericArg = DataType == null ? string.Empty : $"<{DataType}>";
            Verb = Cmdlet.Name.Substring(0, Cmdlet.Name.FindIndex(char.IsUpper, 1));
            Noun = Cmdlet.Name.Substring(Verb.Length);
            CtorArgs = controller.GetImportingConstructorArguments(BaseClass);
            BaseCtorArgs = BaseClass.GetConstructorArguments();
            ImportingConstructorBody = GetImportingConstructorBody(controller);
            CmdletInfo = new CmdletInfo(Cmdlet, Logger);

            DeclaredProperties = Cmdlet.GetPropertiesWithAttribute("ParameterAttribute").Select(p => new GeneratedProperty(p, string.Empty)).ToDictionary(p => p.Name);
            ImplicitProperties = CmdletInfo.GeneratedProperties;

            GenerateProperties();
        }

        private void GenerateProperties()
        {
            //Logger.Log($" - GenerateProperties");

            foreach (var (condition, generator, generatorName) in _generators)
            {
                if (!condition(this))
                {
                    //Logger.Log($"   - {generatorName} [-]");
                    continue;
                };

                foreach (var prop in generator(this))
                {
                    //Logger.Log($"   - {generatorName} [{prop.Name}]");
                    GeneratedProperties.Add(prop.Name, prop);
                }
            }
        }

        private static string GetImportingConstructorBody(INamedTypeSymbol type)
            => string.Join("\n", type.GetPropertiesWithAttribute("ImportAttribute")
                .Select(p => $"            {p.Name} = {p.Name[0].ToString().ToLower()}{p.Name.Substring(1)};"));

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
            yield return new GeneratedProperty("Items", "object", controller.DataType == null ?
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
        protected bool Has_{prop.Name} => Parameters.HasParameter(""{prop.Name}"");
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
            foreach (var prop in controller.DeclaredProperties.Values.Skip(controller.Verb.Equals("Get") && !controller.SkipGetProperty ? 1 : 0))
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
        protected bool Has_{scope} => Parameters.HasParameter(""{scope}"");
        private {scopeType} _{scope};
        protected {scopeType} {scope} => _{scope} ??= Data.Get{scope}();

") { IsScope = true };
        }

        private static readonly ICollection<string> _scopeProperties = new[] { "Server", "Collection", "Project", "Team" };

        private static readonly List<(Predicate<ControllerInfo>, Func<ControllerInfo, IEnumerable<GeneratedProperty>>, string)> _generators =
            new List<(Predicate<ControllerInfo>, Func<ControllerInfo, IEnumerable<GeneratedProperty>>, string)>()
            {
                // Get "input" property

                ((controller) => controller.Verb.Equals("Get") && controller.DeclaredProperties.Count > 0 && !controller.SkipGetProperty, GenerateGetInputProperty, "Get->Input"),

                // Cmdlet declared parameters

                ((controller) => controller.DeclaredProperties.Count > 0, GenerateDeclaredParameters, "Declared Parameters"),

                // Cmdlet implicit (source-gen'ed) parameters

                ((controller) => controller.ImplicitProperties.Count > 0, GenerateImplicitParameters, "Implicit Parameters"),

                // Scope properties

                ((controller) => (int)controller.CmdletInfo.Scope >= (int)CmdletScope.Team, ci => GenerateScopeProperty(ci, CmdletScope.Team, "WebApiTeam"), "Scope properties"),
                ((controller) => (int)controller.CmdletInfo.Scope >= (int)CmdletScope.Project, ci => GenerateScopeProperty(ci, CmdletScope.Project, "WebApiTeamProject"), "Scope properties"),
                ((controller) => (int)controller.CmdletInfo.Scope >= (int)CmdletScope.Collection, ci => GenerateScopeProperty(ci, CmdletScope.Collection, "Models.Connection"), "Scope properties"),
                ((controller) => (int)controller.CmdletInfo.Scope >= (int)CmdletScope.Server, ci => GenerateScopeProperty(ci, CmdletScope.Server, "Models.Connection"), "Scope properties"), 

                // ParameterSetName

                ((controller) => true, GenerateParameterSetProperty, "ParameterSetName"),

                // 'Items' property

                ((controller) => !controller.Verb.Equals("Get") && controller.DeclaredProperties.Count > 0 &&
                    controller.DeclaredProperties.Values.First().Type.Equals("object"), GenerateItemsProperty, "Items"),

                // DataType
                ((controller) => !string.IsNullOrEmpty(controller.GenericArg), GenerateDataType, "DataType"),
            };
    }
}
