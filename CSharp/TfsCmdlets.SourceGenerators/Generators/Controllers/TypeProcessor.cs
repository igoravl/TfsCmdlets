﻿using System.Linq;
using System.Text;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    public class TypeProcessor : BaseTypeProcessor
    {
        protected override void OnInitialize() { }

        public override string GenerateCode()
        {
            Logger.Log(" - Initializing ControllerInfo");

            var controller = new ControllerInfo(Type, Context);
            var props = new StringBuilder();
            var cacheProps = new StringBuilder();

            foreach (var prop in controller.GeneratedProperties.Values)
            {
                props.Append(prop.ToString());

                if (prop.IsHidden || prop.Name.Equals("Items") ||
                    prop.IsScope || (controller.Verb.Equals("Get") &&
                        controller.DeclaredProperties.Count > 0 &&
                        controller.DeclaredProperties.Values.First().Name.Equals(prop.Name))) continue;

                var type = prop.Type.ToString().EndsWith("SwitchParameter") ? "bool" : prop.Type.ToString();
                var initializer = string.IsNullOrEmpty(prop.DefaultValue) ? string.Empty : $", {prop.DefaultValue}";

                cacheProps.Append($"            // {prop.Name}\n");
                cacheProps.Append($"            Has_{prop.Name} = Parameters.HasParameter(\"{prop.Name}\");\n");
                cacheProps.Append($"            {prop.Name} = Parameters.Get<{type}>(\"{prop.Name}\"{initializer});\n\n");
            }

            return $@"namespace {controller.Namespace}
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
";        }
    }
}