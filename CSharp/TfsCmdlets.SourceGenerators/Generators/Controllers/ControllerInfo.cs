using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TfsCmdlets.Services;
using TfsCmdlets.SourceGenerators.Generators.Cmdlets;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    public record ControllerInfo : ClassInfo
    {
        public CmdletInfo CmdletInfo { get; set; }
        public string CmdletClass { get; }
        public string GenericArg { get; }
        public string DataType { get; }
        public string Client { get; }
        public string BaseClass { get; }
        public string CmdletName { get; }
        public string Cmdlet { get; }
        public string Usings { get; }
        private bool SkipGetProperty { get; }
        public string ImportingConstructorBody { get; }
        public string CtorArgs { get; }
        public string BaseCtorArgs { get; }

        public string BaseClassName => BaseClass.Contains('.') ? BaseClass.Split('.').Last() : BaseClass;
        public string Verb => CmdletInfo.Verb;
        public string Noun => CmdletInfo.Noun;

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
            //var baseClass = customBaseClass ??
            //                context.Compilation.GetTypeByMetadataName("TfsCmdlets.Controllers.ControllerBase");
            BaseClass = customBaseClass?.FullName() ?? "TfsCmdlets.Controllers.ControllerBase";

            DataType = controller.GetAttributeConstructorValue<INamedTypeSymbol>("CmdletControllerAttribute").FullName();
            Client = controller.GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "Client").FullName();
            GenericArg = DataType == null ? string.Empty : $"<{DataType}>";

            //CtorArgs = controller.GetImportingConstructorArguments();

            // GenerateProperties();
        }

        public string GenerateUsings()
        {
            return CmdletInfo?.Usings ?? "using System;";
        }

        public string GenerateClientProperty()
        {
            return Client == null ? string.Empty : $$"""
                                                             private {{Client}} Client { get; }
                                                    
                                                     """;
        }

        public string GenerateGetInputProperty()
        {
            if (Verb != "Get") return string.Empty;

            var prop = CmdletInfo.ParameterProperties.First();
            var initializer = string.IsNullOrEmpty(prop.DefaultValue) ? string.Empty : $", {prop.DefaultValue}";

            return $$"""
                             // {{prop.Name}}
                             protected bool Has_{{prop.Name}} => Parameters.HasParameter(nameof({{prop.Name}}));
                             protected IEnumerable {{prop.Name}}
                             {
                                 get
                                 {
                                     var paramValue = Parameters.Get<object>(nameof({{prop.Name}}){{initializer}});
                                     if(paramValue is ICollection col) return col;
                                     return new[] { paramValue };
                                 }
                             }
                     """;
        }
        public string GenerateDeclaredProperties()
        {
            var sb = new StringBuilder();

            foreach (var prop in CmdletInfo.ParameterProperties.Skip(Verb == "Get" ? 1 : 0))
            {
                sb.Append($$"""
                            
                                    // {{prop.Name}}
                                    protected bool Has_{{prop.Name}} { get; set; }
                                    protected {{(prop.Type == "SwitchParameter" ? "bool" : prop.Type)}} {{prop.Name}} { get; set; }
                            
                            """);
            }

            return sb.ToString();
        }

        public string GenerateScopeProperties()
        {
            var scope = CmdletInfo.Scope;
            var sb = new StringBuilder();

            if ((int)scope >= (int)CmdletScope.Team) GenerateScopeProperty(CmdletScope.Team, "WebApiTeam", sb);
            if ((int)scope >= (int)CmdletScope.Project) GenerateScopeProperty(CmdletScope.Project, "WebApiTeamProject", sb);
            if ((int)scope >= (int)CmdletScope.Collection) GenerateScopeProperty(CmdletScope.Collection, "Models.Connection", sb);
            if ((int)scope >= (int)CmdletScope.Server) GenerateScopeProperty(CmdletScope.Server, "Models.Connection", sb);

            return sb.ToString();
        }

        private void GenerateScopeProperty(CmdletScope scope, string scopeType, StringBuilder sb)
        {
            var scopeName = scope.ToString();

            sb.Append($$"""
                         
                                 // {{scopeName}}
                                 protected bool Has_{{scopeName}} => Parameters.HasParameter("{{scopeName}}");
                                 protected {{scopeType}} {{scopeName}} => Data.Get{{scopeName}}();
                         
                         """);
        }

        public string GenerateParameterSetProperty()
        {
            return $$"""
                             // ParameterSetName
                             protected bool Has_ParameterSetName {get;set;}
                             protected string ParameterSetName {get;set;}
                     
                     """;
        }

        public string GenerateItemsProperty()
        {
            var hasItemsProperty = !Verb.Equals("Get")
                                   && CmdletInfo.ParameterProperties.Count > 0
                                   && CmdletInfo.ParameterProperties[0].Type.Equals("object");

            if (!hasItemsProperty) return string.Empty;

            if (string.IsNullOrEmpty(DataType))
            {
                return $"""

                                // Items
                                protected IEnumerable Items => Data.Invoke("Get", "{Noun}");


                        """;
            }

            return $$"""

                                 // Items
                                 protected IEnumerable{{GenericArg}} Items => {{CmdletInfo.ParameterProperties[0].Name}} switch {
                                     {{DataType}} item => new[] { item },
                                     IEnumerable{{GenericArg}} items => items,
                                     _ => Data.GetItems{{GenericArg}}()
                                 };

                     """;

        }

        public string GenerateDataTypeProperty()
        {
            return $"""
                            // DataType
                            public override Type DataType => typeof({DataType});


                    """;
        }

        public string GenerateCacheProperties()
        {
            var sb = new StringBuilder();

            foreach (var prop in CmdletInfo.ParameterProperties.Skip(Verb == "Get" ? 1 : 0))
            {
                sb.Append($$"""

                                            // {{prop.Name}}
                                            Has_{{prop.Name}} = Parameters.HasParameter("{{prop.Name}}");
                                            {{prop.Name}} = Parameters.Get<{{(prop.Type == "SwitchParameter" ? "bool" : prop.Type)}}>("{{prop.Name}}");

                                """);
            }

            sb.Append($$"""
                        
                                    // ParameterSetName
                                    Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
                                    ParameterSetName = Parameters.Get<string>("ParameterSetName");
                        
                        """);

            return sb.ToString();
        }

        internal static ControllerInfo Create(GeneratorAttributeSyntaxContext ctx) =>
            ctx.TargetSymbol is not INamedTypeSymbol controllerSymbol ?
                null :
                new ControllerInfo(controllerSymbol);
    }
}
