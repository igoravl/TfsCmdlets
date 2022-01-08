using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
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
        public string CmdletAttribute { get; private set; }
        public string OutputTypeAttribute { get; private set; }

        public CmdletInfo(INamedTypeSymbol cmdlet)
            : base(cmdlet)
        {
            Verb = cmdlet.Name.Substring(0, cmdlet.Name.FindIndex(c => char.IsUpper(c), 1));
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
            CmdletAttribute = GenerateCmdletAttribute(this);
            OutputTypeAttribute = GenerateOutputTypeAttribute(this);
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
                $"\n  [OutputType(typeof({cmdlet.OutputType.FullName()}))]" :
                $"\n  [OutputType(typeof({cmdlet.DataType.FullName()}))]";
        }
    }
}