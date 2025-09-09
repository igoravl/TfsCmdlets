using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TfsCmdlets.SourceGenerators.Generators.Cmdlets;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    public record ControllerInfo : ClassInfoBase
    {
        public CmdletInfo CmdletInfo { get; private set; }
        public string CmdletClass {get;}
        public string Noun { get; }
        public string GenericArg { get; }
        public string Verb { get; }
        public string DataType { get; }
        public string Client { get; }
        public string BaseClass { get; }
        public string CmdletName { get; }
        public string Cmdlet { get; }
        public string Usings { get; }
        private bool SkipGetProperty { get; }
        private string ImportingConstructorBody { get; }
        private string CtorArgs { get; }
        private string BaseCtorArgs { get; }
        private string BaseClassName => BaseClass.Contains('.') ? BaseClass.Split('.').Last() : BaseClass;

        internal ControllerInfo(INamedTypeSymbol controller)
            : base(controller)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));

            var cmdletClassName = controller.FullName().Replace(".Controllers.", ".Cmdlets.");
            cmdletClassName = cmdletClassName.Substring(0, cmdletClassName.Length - "Controller".Length);
            var customCmdletClass = controller.GetAttributeNamedValue<string>("CmdletControllerAttribute", "CustomCmdletName");
            if (!string.IsNullOrEmpty(customCmdletClass)) cmdletClassName = cmdletClassName.Replace(controller.Name, $"{customCmdletClass}Controller");
            CmdletClass = cmdletClassName;
            CmdletName = cmdletClassName.Substring(cmdletClassName.LastIndexOf('.') + 1);

            var customBaseClass = controller.GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "CustomBaseClass");
            BaseClass = customBaseClass?.FullName() ?? "TfsCmdlets.Controllers.ControllerBase";

            DataType = controller.GetAttributeConstructorValue<INamedTypeSymbol>("CmdletControllerAttribute").FullName();
            Client = controller.GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "Client").FullName();
            GenericArg = DataType == null ? string.Empty : $"<{DataType}>";
            Verb = cmdletClassName.Substring(0, cmdletClassName.FindIndex(char.IsUpper, 1));
            Noun = cmdletClassName.Substring(Verb.Length);

            // GenerateProperties();
        }

        public string GenerateCode(CmdletInfo cmdlet)
        {
            CmdletInfo = cmdlet;
            return GenerateCode();
        }

        public override string GenerateCode()
        {
            return $@"{HEADER}

{GenerateUsings()}

namespace {Namespace}
{{
    //
    internal partial class {Name}: {BaseClassName}
    {{
        // Client property
        {GenerateClientProperty()}

        // Input property
        {GenerateGetInputProperty()}

        // Cmdlet explicit (declared) parameters
        {GenerateDeclaredProperties()}

        // Cmdlet implicit (source-gen'ed) parameters
        {GenerateImplicitProperties()}

        // Scope properties
        {GenerateScopeProperties()}

        // ParameterSetName
        {GenerateParameterSetProperty()}

        // 'Items' property
        {GenerateItemsProperty()}

        // DataType
        {GenerateDataTypeProperty()}

        protected override void CacheParameters()
        {{
            {GenerateCacheProperties()}
        }}

        [ImportingConstructor]
        public {Name}({CtorArgs})
            : base({BaseCtorArgs})
        {{
{ImportingConstructorBody}
        }}
    }}
}}
";
        }

        private string GenerateUsings()
        {
            return CmdletInfo?.Usings ?? "using System;";
        }

        private string GenerateClientProperty()
        {
            return Client == null ? string.Empty : $"private {Client} Client {{ get; }}";
        }

        private string GenerateGetInputProperty()
        {
            var prop = CmdletInfo.DeclaredProperties.First();
            var initializer = string.IsNullOrEmpty(prop.DefaultValue) ? string.Empty : $", {prop.DefaultValue}";

            return $@"        // {prop.Name}
        protected bool Has_{prop.Name} => Parameters.HasParameter(nameof({prop.Name}));
        protected IEnumerable {prop.Name}
        {{
            get
            {{
                var paramValue = Parameters.Get<object>(nameof({prop.Name}){initializer});
                if(paramValue is ICollection col) return col;
                return new[] {{ paramValue }};
            }}
        }}";
}

        private string GenerateDeclaredProperties()
        {
            return string.Empty;
        }

        private string GenerateImplicitProperties()
        {
            return string.Empty;
        }

        private string GenerateScopeProperties()
        {
            return string.Empty;
        }

        private string GenerateParameterSetProperty()
        {
            return string.Empty;
        }

        private string GenerateItemsProperty()
        {
            return string.Empty;
        }

        private string GenerateDataTypeProperty()
        {
            return string.Empty;
        }

        private string GenerateCacheProperties()
        {
            return string.Empty;
        }

        internal static ControllerInfo Create(GeneratorAttributeSyntaxContext ctx) => 
            ctx.TargetSymbol is not INamedTypeSymbol controllerSymbol ? 
                null : 
                new ControllerInfo(controllerSymbol);
    }
}
