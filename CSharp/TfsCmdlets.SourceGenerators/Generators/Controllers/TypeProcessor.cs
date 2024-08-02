using System;
using System.Linq;
using System.Text;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    public class TypeProcessor : BaseTypeProcessor
    {
        private ControllerInfo _controller;

        protected override void OnInitialize()
        {
            try
            {
                _controller = new ControllerInfo(Type, Context, Logger);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, $"Error initializing ControllerInfo for {Type.FullName()}");
                Ignore = true;
            }
        }

        public override string GenerateCode()
        {
            var props = new StringBuilder();
            var cacheProps = new StringBuilder();
            var controller = _controller;
            var clientProp = string.Empty;

            if(controller.Client != null)
            {
                clientProp = $"[Import] private {controller.Client.FullName()} Client {{ get; set; }}";
            }
            
            foreach (var prop in controller.GeneratedProperties.Values)
            {
                props.Append(prop.ToString());

                if (prop.IsHidden || prop.Name.Equals("Items") ||
                    prop.IsScope || (controller.Verb.Equals("Get") &&
                        controller.DeclaredProperties.Count > 0 &&
                        !controller.SkipGetProperty &&
                        controller.DeclaredProperties.Values.First().Name.Equals(prop.Name))) continue;

                var type = prop.Type.ToString().EndsWith("SwitchParameter") ? "bool" : prop.Type.ToString();
                var initializer = string.IsNullOrEmpty(prop.DefaultValue) ? string.Empty : $", {prop.DefaultValue}";

                cacheProps.Append($"            // {prop.Name}\n");
                cacheProps.Append($"            Has_{prop.Name} = Parameters.HasParameter(\"{prop.Name}\");\n");
                cacheProps.Append($"            {prop.Name} = Parameters.Get<{type}>(\"{prop.Name}\"{initializer});\n\n");
            }

            return $@"{controller.Usings}

namespace {controller.Namespace}
{{
    internal partial class {controller.Name}: {controller.BaseClassName}
    {{
        {clientProp}

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
    }
}