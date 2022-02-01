using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    public class CmdletInfo : GeneratorState
    {
        public string Noun { get; private set; }
        public string Verb { get; private set; }
        public CmdletScope Scope { get; private set; }
        public bool SkipAutoProperties { get; private set; }
        public bool DesktopOnly { get; private set; }
        public bool HostedOnly { get; private set; }
        public int RequiresVersion { get; private set; }
        public bool NoAutoPipeline { get; private set; }
        public string DefaultParameterSetName { get; private set; }
        public string CustomControllerName { get; private set; }
        public INamedTypeSymbol DataType { get; private set; }
        public INamedTypeSymbol OutputType { get; private set; }
        public bool SupportsShouldProcess { get; private set; }
        public bool ReturnsValue { get; private set; }
        public bool SkipGetProperty { get; private set; }
        public string CmdletAttribute { get; private set; }
        public string OutputTypeAttribute { get; private set; }

        public CmdletInfo(INamedTypeSymbol cmdlet, Logger logger)
            : base(cmdlet, logger)
        {
            if (cmdlet == null) throw new ArgumentNullException(nameof(cmdlet));

            Verb = cmdlet.Name.Substring(0, cmdlet.Name.FindIndex(char.IsUpper, 1));
            Noun = cmdlet.Name.Substring(Verb.Length);
            Scope = cmdlet.GetAttributeConstructorValue<CmdletScope>("TfsCmdletAttribute");
            SkipAutoProperties = cmdlet.GetAttributeNamedValue<bool>("TfsCmdletAttribute", "SkipAutoProperties");
            DesktopOnly = cmdlet.GetAttributeNamedValue<bool>("TfsCmdletAttribute", "DesktopOnly");
            HostedOnly = cmdlet.GetAttributeNamedValue<bool>("TfsCmdletAttribute", "HostedOnly");
            RequiresVersion = cmdlet.GetAttributeNamedValue<int>("TfsCmdletAttribute", "RequiresVersion");
            NoAutoPipeline = cmdlet.GetAttributeNamedValue<bool>("TfsCmdletAttribute", "NoAutoPipeline");
            DefaultParameterSetName = cmdlet.GetAttributeNamedValue<string>("CmdletAttribute", "DefaultParameterSetName");
            OutputType = cmdlet.GetAttributeNamedValue<INamedTypeSymbol>("TfsCmdletAttribute", "OutputType");
            DataType = cmdlet.GetAttributeNamedValue<INamedTypeSymbol>("TfsCmdletAttribute", "DataType") ?? OutputType;
            SupportsShouldProcess = cmdlet.GetAttributeNamedValue<bool>("TfsCmdletAttribute", "SupportsShouldProcess");
            DefaultParameterSetName = cmdlet.GetAttributeNamedValue<string>("TfsCmdletAttribute", "DefaultParameterSetName");
            CustomControllerName = cmdlet.GetAttributeNamedValue<string>("TfsCmdletAttribute", "CustomControllerName");
            ReturnsValue = cmdlet.GetAttributeNamedValue<bool>("TfsCmdletAttribute", "ReturnsValue");
            SkipGetProperty = cmdlet.GetAttributeNamedValue<bool>("TfsCmdletAttribute", "SkipGetProperty");
            CmdletAttribute = GenerateCmdletAttribute(this);
            OutputTypeAttribute = GenerateOutputTypeAttribute(this);

            GenerateProperties();
        }

        private void GenerateProperties()
        {
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

        private static string GenerateCmdletAttribute(CmdletInfo cmdlet)
        {
            var props = new List<string>();

            if (cmdlet.SupportsShouldProcess) props.Add($"SupportsShouldProcess = true");
            if (!string.IsNullOrEmpty(cmdlet.DefaultParameterSetName)) props.Add($"DefaultParameterSetName = \"{cmdlet.DefaultParameterSetName}\"");

            return $"[Cmdlet(\"{cmdlet.Verb}\", \"Tfs{cmdlet.Noun}\"{(props.Any() ? $", {string.Join(", ", props)}" : string.Empty)})]";
        }

        private static string GenerateOutputTypeAttribute(CmdletInfo cmdlet)
        {
            if (cmdlet.OutputType == null && cmdlet.DataType == null) return string.Empty;

            return cmdlet.OutputType != null ?
                $"\n    [OutputType(typeof({cmdlet.OutputType.FullName()}))]" :
                $"\n    [OutputType(typeof({cmdlet.DataType.FullName()}))]";
        }

        private static IEnumerable<GeneratedProperty> GenerateScopeProperty(CmdletScope currentScope, CmdletInfo settings)
        {
            var scopeName = currentScope.ToString();
            var isGetScopedCmdlet = IsGetScopeCmdlet(settings);
            var isPipeline = IsPipelineProperty(currentScope, settings);
            var valueFromPipeline = isPipeline ? "ValueFromPipeline=true" : string.Empty;
            var parameterSetNames = isGetScopedCmdlet ? _credentialParameterSetNames.Select(s => $"ParameterSetName=\"{s}\"") : new[] { string.Empty };
            var attributes = new List<(string, string)>();

            if (scopeName.Equals("Collection"))
            {
                attributes.Add(("Alias", "\"Organization\""));
            }

            if (isGetScopedCmdlet)
            {
                attributes.Add(("Parameter", $"ParameterSetName=\"{settings.DefaultParameterSetName}\"{(isPipeline ? ", " : string.Empty)}{valueFromPipeline}"));
            }

            foreach (var parameterSetName in parameterSetNames)
            {
                attributes.Add(("Parameter", $"{parameterSetName}{(isGetScopedCmdlet && isPipeline ? ", " : string.Empty)}{valueFromPipeline}"));
            }

            var parm = GenerateParameter(scopeName, "object", attributes, $"HELP_PARAM_{scopeName.ToUpper()}");
            parm.IsScope = true;

            yield return parm;
        }

        private static IEnumerable<GeneratedProperty> GenerateCredentialProperties(CmdletInfo settings)
        {
            yield return GenerateParameter("Cached", "SwitchParameter", "ParameterSetName = \"Cached credentials\", Mandatory = true", "HELP_PARAM_CACHED_CREDENTIAL");

            yield return GenerateParameter("UserName", "string", "ParameterSetName = \"User name and password\", Mandatory = true", "HELP_PARAM_USER_NAME");

            yield return GenerateParameter("Password", "System.Security.SecureString", "ParameterSetName = \"User name and password\", Mandatory = true", "HELP_PARAM_PASSWORD");

            yield return GenerateParameter("Credential", "object", new List<(string, string)>() {
                ("Parameter", "ParameterSetName = \"Credential object\", Mandatory = true"),
                ("ValidateNotNull", string.Empty)}, "HELP_PARAM_CREDENTIAL");

            yield return GenerateParameter("PersonalAccessToken", "string", new List<(string, string)>() {
                ("Parameter", "ParameterSetName = \"Personal Access Token\", Mandatory = true"),
                ("Alias", "\"Pat\"")}, "HELP_PARAM_PERSONAL_ACCESS_TOKEN");

            yield return GenerateParameter("Interactive", "SwitchParameter", "ParameterSetName = \"Prompt for credential\"", "HELP_PARAM_INTERACTIVE");
        }

        private static IEnumerable<GeneratedProperty> GenerateCustomControllerProperty(CmdletInfo settings)
        {
            yield return new GeneratedProperty("CommandName", "string", true, $@"
        protected override string CommandName => ""{settings.CustomControllerName}"";
");
        }

        private static IEnumerable<GeneratedProperty> GenerateReturnsValueProperty(CmdletInfo settings)
        {
            yield return new GeneratedProperty("ReturnsValue", "bool", true, $@"
        protected override bool ReturnsValue => {settings.ReturnsValue.ToString().ToLower()};
");
        }

        private static IEnumerable<GeneratedProperty> GenerateStructureGroupProperty(CmdletInfo settings)
        {
            yield return new GeneratedProperty("StructureGroup", "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup", $@"
        [Parameter]
        internal Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup StructureGroup => 
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.{(settings.Name.EndsWith("Area") ? "Areas" : "Iterations")};
");
        }

        private static IEnumerable<GeneratedProperty> GenerateParameterAsList(string name, string type, string parameterAttributeValues, string helpText)
            => GenerateParameterAsList(name, type, new List<(string, string)>() { ("Parameter", parameterAttributeValues) }, helpText);

        private static IEnumerable<GeneratedProperty> GenerateParameterAsList(string name, string type, IList<(string, string)> attributes, string helpText)
        {
            yield return GenerateParameter(name, type, attributes, helpText);
        }

        private static GeneratedProperty GenerateParameter(string name, string type, string parameterAttributeValues, string helpText)
            => GenerateParameter(name, type, new List<(string, string)>() { ("Parameter", parameterAttributeValues) }, helpText);

        private static GeneratedProperty GenerateParameter(string name, string type, IList<(string, string)> attributes, string helpText)
            => new GeneratedProperty(name, type, $@"
        /// <summary>
        /// {helpText}
        /// </summary>
{string.Join("\n", attributes?.Select(kv => $"        [{kv.Item1}({kv.Item2})]") ?? new[] { "        [Parameter]" })}
        public {type} {name} {{ get; set; }}
");

        private static bool IsGetScopeCmdlet(CmdletInfo cmdlet)
            => _scopeNames.Contains(cmdlet.Noun) && cmdlet.Verb.Equals("Get");

        private static bool IsPipelineProperty(CmdletScope currentScope, CmdletInfo cmdlet)
            => !cmdlet.NoAutoPipeline && 
                (cmdlet.Verb.Equals("Get") || 
                 cmdlet.Verb.StartsWith("Connect") || 
                 cmdlet.Verb.StartsWith("Export")
                ) && ((int)cmdlet.Scope == (int)currentScope);

        private static readonly string[] _scopeNames = new[]{
            "ConfigurationServer", "Organization", "TeamProjectCollection", "TeamProject", "Team" };
        private static readonly string[] _credentialParameterSetNames = new[]{
            "Cached credentials", "User name and password", "Credential object", "Personal Access Token", "Prompt for credential" };


        private static readonly List<(Predicate<CmdletInfo>, Func<CmdletInfo, IEnumerable<GeneratedProperty>>, string)> _generators =
            new List<(Predicate<CmdletInfo>, Func<CmdletInfo, IEnumerable<GeneratedProperty>>, string)>()
            {
                // Basic properties

                ((cmdlet) => cmdlet.Verb == "Rename",
                    ci => GenerateParameterAsList("NewName", "string", "Position = 1, Mandatory = true", "HELP_PARAM_NEWNAME"), "Rename->NewName"),
                ((cmdlet) => cmdlet.Verb == "New" && cmdlet.Noun != "Credential",
                    ci => GenerateParameterAsList("Passthru", "SwitchParameter", string.Empty, "HELP_PARAM_PASSTHRU"), "New->Passthru"),
                ((cmdlet) => cmdlet.Verb == "Set",
                    ci => GenerateParameterAsList("Passthru", "SwitchParameter", string.Empty, "HELP_PARAM_PASSTHRU"), "Set->Passthru"),
                ((cmdlet) => cmdlet.Verb == "Connect",
                    ci => GenerateParameterAsList("Passthru", "SwitchParameter", string.Empty, "HELP_PARAM_PASSTHRU"), "Connect->Passthru"),
                ((cmdlet) => cmdlet.Verb == "Enable",
                    ci => GenerateParameterAsList("Passthru", "SwitchParameter", string.Empty, "HELP_PARAM_PASSTHRU"), "Enable->Passthru"),
                ((cmdlet) => cmdlet.Verb == "Disable",
                    ci => GenerateParameterAsList("Passthru", "SwitchParameter", string.Empty, "HELP_PARAM_PASSTHRU"), "Disable->Passthru"), 

                // Scope properties

                ((cmdlet) => (int)cmdlet.Scope >= (int)CmdletScope.Team, ci => GenerateScopeProperty(CmdletScope.Team, ci), "Scope properties"),
                ((cmdlet) => (int)cmdlet.Scope >= (int)CmdletScope.Project, ci => GenerateScopeProperty(CmdletScope.Project, ci), "Scope properties"),
                ((cmdlet) => (int)cmdlet.Scope >= (int)CmdletScope.Collection, ci => GenerateScopeProperty(CmdletScope.Collection, ci), "Scope properties"),
                ((cmdlet) => (int)cmdlet.Scope >= (int)CmdletScope.Server, ci => GenerateScopeProperty(CmdletScope.Server, ci), "Scope properties"), 

                // Credential properties

                ((cmdlet) => cmdlet.Verb == "Connect", GenerateCredentialProperties, "Connect->Credential"),
                ((cmdlet) => IsGetScopeCmdlet(cmdlet), GenerateCredentialProperties, "(IsScope)->Credential"),
                ((cmdlet) => cmdlet.Name == "NewCredential", GenerateCredentialProperties, "NewCredential->Credential"), 

                // CustomController property
                ((cmdlet) => !string.IsNullOrEmpty(cmdlet.CustomControllerName), GenerateCustomControllerProperty, "CustomController"), 

                // ReturnsValue property
                ((cmdlet) => cmdlet.ReturnsValue, GenerateReturnsValueProperty, "ReturnsValue"), 

                // Areas/Iterations StructureGroup property
                ((cmdlet) => cmdlet.Name.EndsWith("Area") || cmdlet.Name.EndsWith("Iteration"), GenerateStructureGroupProperty, "(Area/Iteration)->StructureGroup"),
            };
    }
}