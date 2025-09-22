using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
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
        public ClassInfo CustomBaseClass { get; set; }
        public string CmdletName { get; }
        public string Cmdlet { get; }
        public string Usings { get; }
        private bool SkipGetProperty { get; }
        public string CustomCmdletName { get; }
        public string CtorArgs { get; }
        public string Verb => CmdletInfo.Verb;
        public string Noun => CmdletInfo.Noun;
        public string ImportingBaseArgs { get; set; }
        internal ControllerInfo(INamedTypeSymbol controller)
            : base(controller)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));

                var cmdletClassName = controller.FullName().Replace(".Controllers.", ".Cmdlets.");
                cmdletClassName = cmdletClassName.Substring(0, cmdletClassName.Length - "Controller".Length);
                CustomCmdletName = controller.GetAttributeNamedValue<string>("CmdletControllerAttribute", "CustomCmdletName");
                if (!string.IsNullOrEmpty(CustomCmdletName))
                    cmdletClassName = cmdletClassName.Replace(controller.Name, $"{CustomCmdletName}Controller");
                CmdletClass = cmdletClassName;
                CmdletName = cmdletClassName.Substring(cmdletClassName.LastIndexOf('.') + 1);

                var customBaseClass =
                    controller.GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "CustomBaseClass");
                BaseClassFullName = customBaseClass?.FullName() ?? "TfsCmdlets.Controllers.ControllerBase";

                DataType = controller.GetAttributeConstructorValue<INamedTypeSymbol>("CmdletControllerAttribute")?
                    .FullName();
                var clientName = controller
                    .GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "Client").FullName();
                //if (clientName.IndexOf('.') < 0)
                //{
                //    clientName = $"TfsCmdlets.HttpClients.{clientName}";
                //}

                Client = clientName;
                GenericArg = (DataType == null ? string.Empty : $"<{DataType}>");
                ImportingBaseArgs = GetImportingBaseArgs();
                CtorArgs = string.Join(", ", GetCtorArgs(controller).ToArray());
                // GenerateProperties();
        }

        public void SetBaseClass(ClassInfo baseClass)
        {
            if (baseClass == null) return;

            CustomBaseClass = baseClass;
            BaseClassFullName = baseClass.FullName;
            BaseClassImportingCtorArgs = baseClass.ImportingConstructorArgs;
            BaseClassImportingBaseArgs = ConvertToArgValues(BaseClassImportingCtorArgs);
            ImportingConstructorArgs = BaseClassImportingCtorArgs;
            ImportingBaseArgs = GetImportingBaseArgs();
        }

        private string ConvertToArgValues(string methodArgs)
        {
            var tokens = Regex.Split(methodArgs, " ?, ?");

            return string.Join(", ", tokens.Select(t => t.Split(' ')[1]).ToArray());
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

        public bool HasGetInputProperty
            => Verb == "Get"
               && CmdletInfo.ParameterProperties.Count > 0
               && CmdletInfo.ParameterProperties[0].Type == "object";

        public string GenerateGetInputProperty()
        {
            if (!HasGetInputProperty) return string.Empty;

            var prop = CmdletInfo.ParameterProperties.FirstOrDefault();

            if (prop is null) return string.Empty;

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

            foreach (var prop in CmdletInfo.ParameterProperties.Skip(HasGetInputProperty ? 1 : 0))
            {
                sb.Append($$"""
                            
                                    // {{prop.Name}}
                                    protected bool Has_{{prop.Name}} { get; set; }
                                    protected {{(prop.Type == "SwitchParameter" ? "bool" : prop.Type)}} {{prop.Name}} { get; set; }
                            
                            """);
            }

            return sb.ToString();
        }

        public bool IsPassthru =>
            Verb switch
            {
                "New" when Noun != "Credential" => true,
                "Set" or "Connect" or "Enable" or "Disable" => true,
                _ => false
            };

        public string GenerateAutomaticProperties()
        {
            var sb = new StringBuilder();

            if(IsPassthru)
            {
                sb.Append("""

                                  // Passthru
                                  protected bool Has_Passthru { get; set; }
                                  protected bool Passthru { get; set; }

                          """);
            }

            if (CmdletInfo.Name.EndsWith("Area") || CmdletInfo.Name.EndsWith("Iteration"))
            {
                sb.Append("""

                                  // StructureGroup
                                  protected bool Has_StructureGroup { get; set; }
                                  protected Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup StructureGroup { get; set; }

                          """);
            }

            if (CmdletInfo.Verb == "Rename")
            {
                sb.Append("""

                                  // NewName
                                  protected bool Has_NewName { get; set; }
                                  protected string NewName { get; set; }

                          """);
            }

            if (HasCredentialProperties)
            {
                sb.Append("""

                                  // Cached
                                  protected bool Has_Cached { get; set; }
                                  protected bool Cached { get; set; }

                                  // UserName
                                  protected bool Has_UserName { get; set; }
                                  protected string UserName { get; set; }

                                  // Password
                                  protected bool Has_Password { get; set; }
                                  protected System.Security.SecureString Password { get; set; }

                                  // Credential
                                  protected bool Has_Credential { get; set; }
                                  protected object Credential { get; set; }

                                  // PersonalAccessToken
                                  protected bool Has_PersonalAccessToken { get; set; }
                                  protected string PersonalAccessToken { get; set; }

                                  // Interactive
                                  protected bool Has_Interactive { get; set; }
                                  protected bool Interactive { get; set; }

                          """);
            }

            return sb.ToString();
        }

        private bool HasCredentialProperties =>
            CmdletInfo.Verb == "Connect"
            || CmdletInfo.IsGetScopeCmdlet 
            || CmdletInfo.Name == "NewCredential";

        private void GenerateCredentialProperties(StringBuilder sb)
        {
            sb.Append("""

                      protected bool Has_Cached { get; set; } // => Parameters.HasParameter(nameof(Cached));
                      protected bool Cached { get; set; } // => _Cached; // Parameters.Get<bool>(nameof(Cached));

                      // UserName
                      protected bool Has_UserName { get; set; } // => Parameters.HasParameter(nameof(UserName));
                      protected string UserName { get; set; } // => _UserName; // Parameters.Get<string>(nameof(UserName));

                      // Password
                      protected bool Has_Password { get; set; } // => Parameters.HasParameter(nameof(Password));
                      protected System.Security.SecureString Password { get; set; } // => _Password; // Parameters.Get<System.Security.SecureString>(nameof(Password));

                      // Credential
                      protected bool Has_Credential { get; set; } // => Parameters.HasParameter(nameof(Credential));
                      protected object Credential { get; set; } // => _Credential; // Parameters.Get<object>(nameof(Credential));

                      // PersonalAccessToken
                      protected bool Has_PersonalAccessToken { get; set; } // => Parameters.HasParameter(nameof(PersonalAccessToken));
                      protected string PersonalAccessToken { get; set; } // => _PersonalAccessToken; // Parameters.Get<string>(nameof(PersonalAccessToken));

                      // Interactive
                      protected bool Has_Interactive { get; set; } // => Parameters.HasParameter(nameof(Interactive));
                      protected bool Interactive { get; set; } // => _Interactive; // Parameters.Get<bool>(nameof(Interactive));

                      """);
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

            sb.Append($"""
                         
                                 // {scopeName}
                                 protected bool Has_{scopeName} => Parameters.HasParameter("{scopeName}");
                                 protected {scopeType} {scopeName} => Data.Get{scopeName}();
                         
                         """);
        }

        public string GenerateParameterSetProperty()
        {
            return """
                           // ParameterSetName
                           protected bool Has_ParameterSetName { get; set; }
                           protected string ParameterSetName { get; set; }

                   """;
        }

        public string GenerateItemsProperty()
        {
            if (!HasItemsProperty) return string.Empty;

            var dataType = DataType ?? CmdletInfo.DataType;

            if (string.IsNullOrEmpty(dataType))
            {
                return $"""

                                // Items
                                protected IEnumerable Items => Data.Invoke("Get", "{Noun}");

                        """;
            }

            return $$"""

                             // Items
                             protected IEnumerable<{{dataType}}> Items => {{CmdletInfo.ParameterProperties[0].Name}} switch {
                                 {{dataType}} item => new[] { item },
                                 IEnumerable<{{dataType}}> items => items,
                                 _ => Data.GetItems<{{dataType}}>()
                             };
                     
                     """;

        }

        private bool HasItemsProperty =>
            !Verb.Equals("Get")
            && CmdletInfo.ParameterProperties.Count > 0
            && CmdletInfo.ParameterProperties[0].Type.Equals("object");

        public string GenerateDataTypeProperty()
        {
            //var dataType = DataType ?? CmdletInfo.DataType;
            if (DataType is null) return string.Empty;

            return $"""
                    
                            // DataType
                            public override Type DataType => typeof({DataType});

                    """;
        }

        public string GenerateCacheProperties()
        {
            var sb = new StringBuilder();

            foreach (var prop in CmdletInfo.ParameterProperties.Skip(HasGetInputProperty ? 1 : 0))
            {
                var initValue = string.IsNullOrEmpty(prop.DefaultValue)
                    ? string.Empty 
                    : $", {prop.DefaultValue}";

                sb.Append($"""
                                            // {prop.Name}
                                            Has_{prop.Name} = Parameters.HasParameter("{prop.Name}");
                                            {prop.Name} = Parameters.Get<{(prop.Type == "SwitchParameter" ? "bool" : prop.Type)}>("{prop.Name}"{initValue});
                                
                                """);
                sb.AppendLine();
            }

            if (IsPassthru)
            {
                sb.Append("""
                                      // Passthru
                                      Has_Passthru = Parameters.HasParameter("Passthru");
                                      Passthru = Parameters.Get<bool>("Passthru");

                          """);
                sb.AppendLine();
            }

            if (CmdletInfo.Verb == "Rename")
            {
                sb.Append("""
                                      // NewName
                                      Has_NewName = Parameters.HasParameter("NewName");
                                      NewName = Parameters.Get<string>("NewName");

                          """);
                sb.AppendLine();
            }

            if (CmdletInfo.Name.EndsWith("Area") || CmdletInfo.Name.EndsWith("Iteration"))
            {
                sb.Append("""
                                      // StructureGroup
                                      Has_StructureGroup = Parameters.HasParameter("StructureGroup");
                                      StructureGroup = Parameters.Get<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup>("StructureGroup");


                          """);
            }

            if (HasCredentialProperties)
            {
                sb.Append("""
                                      // Cached
                                      Has_Cached = Parameters.HasParameter("Cached");
                                      Cached = Parameters.Get<bool>("Cached");
                          
                                      // UserName
                                      Has_UserName = Parameters.HasParameter("UserName");
                                      UserName = Parameters.Get<string>("UserName");
                          
                                      // Password
                                      Has_Password = Parameters.HasParameter("Password");
                                      Password = Parameters.Get<System.Security.SecureString>("Password");
                          
                                      // Credential
                                      Has_Credential = Parameters.HasParameter("Credential");
                                      Credential = Parameters.Get<object>("Credential");
                          
                                      // PersonalAccessToken
                                      Has_PersonalAccessToken = Parameters.HasParameter("PersonalAccessToken");
                                      PersonalAccessToken = Parameters.Get<string>("PersonalAccessToken");
                          
                                      // Interactive
                                      Has_Interactive = Parameters.HasParameter("Interactive");
                                      Interactive = Parameters.Get<bool>("Interactive");
                          
                          """);
                sb.AppendLine();
            }

            sb.Append("""
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
