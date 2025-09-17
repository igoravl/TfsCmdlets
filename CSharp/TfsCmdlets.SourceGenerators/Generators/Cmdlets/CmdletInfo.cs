using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    public record CmdletInfo : ClassInfo
    {
        public string Noun { get; init; }
        public string Verb { get; init; }
        public CmdletScope Scope { get; init; }
        public bool SkipAutoProperties { get; init; }
        public bool DesktopOnly { get; init; }
        public bool HostedOnly { get; init; }
        public int RequiresVersion { get; init; }
        public bool NoAutoPipeline { get; init; }
        public string DefaultParameterSetName { get; init; }
        public string CustomControllerName { get; init; }
        public string DataType { get; init; }
        public string OutputType { get; init; }
        public bool SupportsShouldProcess { get; init; }
        public bool ReturnsValue { get; init; }
        public bool SkipGetProperty { get; init; }
        public string AdditionalCredentialParameterSets { get; init; }
        public bool WindowsOnly { get; init; }
        public string Usings { get; init; }
        public EquatableArray<ParameterInfo> ParameterProperties { get; set; }

        public CmdletInfo(INamedTypeSymbol cmdlet)
            : base(cmdlet)
        {
            if (cmdlet == null) throw new ArgumentNullException(nameof(cmdlet));

            Verb = cmdlet.Name.Substring(0, cmdlet.Name.FindIndex(char.IsUpper, 1));
            Noun = cmdlet.Name.Substring(Verb.Length);

            var attr = cmdlet.GetAttribute("TfsCmdletAttribute");
            if (attr == null) throw new ArgumentException($"Class {cmdlet.Name} is not a TfsCmdlet");

            Scope = attr.GetAttributeConstructorValue<CmdletScope>();
            SkipAutoProperties = attr.GetAttributeNamedValue<bool>("SkipAutoProperties");
            DesktopOnly = attr.GetAttributeNamedValue<bool>("DesktopOnly");
            WindowsOnly = attr.GetAttributeNamedValue<bool>("WindowsOnly");
            HostedOnly = attr.GetAttributeNamedValue<bool>("HostedOnly");
            RequiresVersion = attr.GetAttributeNamedValue<int>("RequiresVersion");
            NoAutoPipeline = attr.GetAttributeNamedValue<bool>("NoAutoPipeline");
            DefaultParameterSetName = attr.GetAttributeNamedValue<string>("DefaultParameterSetName");
            OutputType = attr.GetAttributeNamedValue<INamedTypeSymbol>("OutputType")?.FullName();
            DataType = attr.GetAttributeNamedValue<INamedTypeSymbol>("DataType")?.FullName() ?? OutputType;
            CustomControllerName = attr.GetAttributeNamedValue<string>("CustomControllerName");
            ReturnsValue = attr.GetAttributeNamedValue<bool>("ReturnsValue");
            SkipGetProperty = attr.GetAttributeNamedValue<bool>("SkipGetProperty");
            AdditionalCredentialParameterSets = attr.GetAttributeNamedValue<string>("AdditionalCredentialParameterSets");
            Usings = cmdlet.GetDeclaringSyntax<TypeDeclarationSyntax>().FindParentOfType<CompilationUnitSyntax>()?.Usings.ToString();
            SupportsShouldProcess = attr.HasAttributeNamedValue("SupportsShouldProcess")
                ? attr.GetAttributeNamedValue<bool>("SupportsShouldProcess")
                : "GetTestSearchConnectDisconnect".IndexOf(Verb, StringComparison.Ordinal) < 0;

            ParameterProperties = GetParameterProperties(cmdlet);
        }

        public string CmdletAttribute
        {
            get
            {
                var sb = new StringBuilder($"[Cmdlet(\"{Verb}\", \"Tfs{Noun}\"");

                if (SupportsShouldProcess) sb.Append(", SupportsShouldProcess = true");
                if (!string.IsNullOrEmpty(DefaultParameterSetName)) sb.Append($", DefaultParameterSetName = \"{DefaultParameterSetName}\"");
                sb.Append(")]");

                return sb.ToString();
            }
        }

        public string OutputTypeAttribute
        {
            get
            {
                if (OutputType == null && DataType == null) return string.Empty;

                return OutputType != null
                    ? $"\n    [OutputType(typeof({OutputType}))]"
                    : $"\n    [OutputType(typeof({DataType}))]";
            }
        }

        public string GenerateProperties()
        {
            var sb = new StringBuilder();

            // Basic properties

            switch (Verb)
            {
                case "Rename":
                    {
                        sb.Append(GenerateParameter("NewName", "string", "Position = 1, Mandatory = true",
                            "HELP_PARAM_NEWNAME"));
                        break;
                    }
                case "New" when Noun != "Credential":
                case "Set":
                case "Connect":
                case "Enable":
                case "Disable":
                    {
                        sb.Append(GenerateParameter("Passthru", "SwitchParameter", string.Empty,
                            "HELP_PARAM_PASSTHRU"));
                        break;
                    }
            }

            // Scope properties

            if ((int)Scope >= (int)CmdletScope.Team) GenerateScopeProperty(CmdletScope.Team, sb);
            if ((int)Scope >= (int)CmdletScope.Project) GenerateScopeProperty(CmdletScope.Project, sb);
            if ((int)Scope >= (int)CmdletScope.Collection) GenerateScopeProperty(CmdletScope.Collection, sb);
            if ((int)Scope >= (int)CmdletScope.Server) GenerateScopeProperty(CmdletScope.Server, sb);

            // Credential properties

            if (Verb == "Connect") GenerateCredentialProperties(sb);
            if (IsGetScopeCmdlet) GenerateCredentialProperties(sb);
            if (Name == "NewCredential") GenerateCredentialProperties(sb);

            // CustomController property

            if (!string.IsNullOrEmpty(CustomControllerName)) GenerateCustomControllerProperty(sb);

            // ReturnsValue property
            if (ReturnsValue) GenerateReturnsValueProperty(sb);

            // Areas/Iterations StructureGroup property

            if (Name.EndsWith("Area") || Name.EndsWith("Iteration")) GenerateStructureGroupProperty(sb);

            return sb.ToString();
        }

        private void GenerateScopeProperty(CmdletScope currentScope, StringBuilder sb)
        {
            var scopeName = currentScope.ToString();
            var isPipeline = IsPipelineProperty(currentScope);
            var valueFromPipeline = isPipeline ? "ValueFromPipeline=true" : string.Empty;
            var additionalParameterSets = AdditionalCredentialParameterSets?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            var parameterSetNames = IsGetScopeCmdlet ? CredentialParameterSetNames.Union(additionalParameterSets).Select(s => $"ParameterSetName=\"{s}\"") : new[] { string.Empty };
            var attributes = new List<(string, string)>();

            if (scopeName.Equals("Collection"))
            {
                attributes.Add(("Alias", "\"Organization\""));
            }

            if (IsGetScopeCmdlet)
            {
                attributes.Add(("Parameter", $"ParameterSetName=\"{DefaultParameterSetName}\"{(isPipeline ? ", " : string.Empty)}{valueFromPipeline}"));
            }

            attributes.AddRange(parameterSetNames.Select(parameterSetName => ("Parameter", $"{parameterSetName}{(IsGetScopeCmdlet && isPipeline ? ", " : string.Empty)}{valueFromPipeline}")));

            sb.Append(GenerateParameter(scopeName, "object", attributes, $"HELP_PARAM_{scopeName.ToUpper()}"));
        }

        private void GenerateCredentialProperties(StringBuilder sb)
        {
            sb.Append(GenerateParameter("Cached", "SwitchParameter", "ParameterSetName = \"Cached credentials\", Mandatory = true", "HELP_PARAM_CACHED_CREDENTIAL"));
            sb.Append(GenerateParameter("UserName", "string", "ParameterSetName = \"User name and password\", Mandatory = true", "HELP_PARAM_USER_NAME"));
            sb.Append(GenerateParameter("Password", "System.Security.SecureString", "ParameterSetName = \"User name and password\", Mandatory = true", "HELP_PARAM_PASSWORD"));
            sb.Append(GenerateParameter("Credential", "object", new List<(string, string)>() {
                ("Parameter", "ParameterSetName = \"Credential object\", Mandatory = true"),
                ("ValidateNotNull", string.Empty)}, "HELP_PARAM_CREDENTIAL"));
            sb.Append(GenerateParameter("PersonalAccessToken", "string", new List<(string, string)>() {
                ("Parameter", "ParameterSetName = \"Personal Access Token\", Mandatory = true"),
                ("Alias", "\"Pat\"")}, "HELP_PARAM_PERSONAL_ACCESS_TOKEN"));
            sb.Append(GenerateParameter("Interactive", "SwitchParameter", "ParameterSetName = \"Prompt for credential\"", "HELP_PARAM_INTERACTIVE"));
        }

        private void GenerateCustomControllerProperty(StringBuilder sb)
        {
            sb.Append($"""
                               protected override string CommandName => "{CustomControllerName}";
                       """);
        }

        private void GenerateReturnsValueProperty(StringBuilder sb)
        {
            sb.Append($"        protected override bool ReturnsValue => {ReturnsValue.ToString().ToLower()};");
        }

        private void GenerateStructureGroupProperty(StringBuilder sb)
        {
            sb.Append($"""
                               [Parameter]
                               internal Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup StructureGroup => 
                                   Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.{(Name.EndsWith("Area") ? "Areas" : "Iterations")};
                       """);
        }

        private static string GenerateParameter(string name, string type, string parameterAttributeValues, string helpText)
            => GenerateParameter(name, type, new List<(string, string)>() { ("Parameter", parameterAttributeValues) }, helpText);

        private static string GenerateParameter(string name, string type, IList<(string, string)> attributes, string helpText)
            => $$"""

                         /// <summary>
                         /// {{helpText}}
                         /// </summary>
                 {{string.Join("\n", attributes?.Select(kv => $"        [{kv.Item1}({kv.Item2})]") ?? new[] { "        [Parameter]" })}}
                         public {{type}} {{name}} { get; set; }

                 """;

        private bool IsGetScopeCmdlet
            => ScopeNames.Contains(Noun) && Verb.Equals("Get");

        private bool IsPipelineProperty(CmdletScope currentScope)
            => !NoAutoPipeline && "GetConnectExport".IndexOf(Verb, StringComparison.Ordinal) >=0 && ((int)Scope == (int)currentScope);

        private static readonly string[] ScopeNames = new[]{
            "ConfigurationServer", "Organization", "TeamProjectCollection", "TeamProject", "Team" };

        private static readonly string[] CredentialParameterSetNames = new[]{
            "Cached credentials", "User name and password", "Credential object", "Personal Access Token", "Prompt for credential" };

        private static EquatableArray<ParameterInfo> GetParameterProperties(INamedTypeSymbol cmdlet)
        {
            var props = cmdlet
                .GetPropertiesWithAttribute("Parameter") // TODO
                .Select(p => new ParameterInfo(p)).ToList();

            return new EquatableArray<ParameterInfo>(props.Count > 0 ?
                props.ToArray() :
                Array.Empty<ParameterInfo>());
        }

        internal static CmdletInfo Create(GeneratorAttributeSyntaxContext ctx)
            => ctx.TargetSymbol is not INamedTypeSymbol cmdletSymbol
                ? null
                : new CmdletInfo(cmdletSymbol);
    }
}