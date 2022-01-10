using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    public class ControllerInfo : GeneratorState
    {
        public string Noun { get; }
        public CmdletInfo CmdletInfo { get; }
        public string GenericArg { get; }
        public INamedTypeSymbol CustomBaseClass { get; }
        internal string Verb { get; }
        public INamedTypeSymbol DataType { get; }
        public INamedTypeSymbol BaseClass { get; }
        public bool IsGeneric { get; }
        public string CmdletName { get; }
        public INamedTypeSymbol Cmdlet { get; }
        public string CtorArgs { get; }
        public string BaseCtorArgs { get; }
        public string ImportingConstructorBody { get; }
        public IList<IPropertySymbol> DeclaredProperties { get; }
        public IDictionary<string, GeneratedProperty> ImplicitProperties { get; }
        public string PropertyDeclarations { get; }
        public string ItemsProperty { get; }
        public string BaseClassName => BaseClass.Name;
        
        internal ControllerInfo(INamedTypeSymbol controller, IDictionary<string, GeneratorState> generatorStates, GeneratorExecutionContext context)
            : base(controller)
        {
            var genericTypeArg = controller.GetAttributeConstructorValue<INamedTypeSymbol>("CmdletControllerAttribute");
            var customBaseClass = controller.GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "CustomBaseClass");
            var customCmdletName = controller.GetAttributeNamedValue<string>("CmdletControllerAttribute", "CustomCmdletName");
            var cmdletName = controller.FullName().Replace(".Controllers.", ".Cmdlets."); //.Replace("Controller", string.Empty);

            CmdletName = string.IsNullOrEmpty(customCmdletName) ? cmdletName.Substring(0, cmdletName.Length - "Controller".Length) : cmdletName.Replace(controller.Name, customCmdletName);
            Cmdlet = context.Compilation.GetTypeByMetadataName(CmdletName);

            if (Cmdlet == null) throw new ArgumentException($"Unable to find cmdlet class '{CmdletName}'");

            BaseClass = GetBaseClass(context, controller);
            IsGeneric = customBaseClass == null && genericTypeArg != null;
            DataType = IsGeneric ? genericTypeArg : null;
            GenericArg = IsGeneric ? $"<{genericTypeArg}>" : string.Empty;
            Verb = Cmdlet.Name.Substring(0, Cmdlet.Name.FindIndex(char.IsUpper, 1));
            Noun = Cmdlet.Name.Substring(Verb.Length);
            CtorArgs = controller.GetImportingConstructorArguments(BaseClass);
            BaseCtorArgs = BaseClass.GetConstructorArguments();
            ImportingConstructorBody = GetImportingConstructorBody(controller);
            CmdletInfo = (CmdletInfo)generatorStates[Cmdlet.FullName()];
            DeclaredProperties = Cmdlet.GetPropertiesWithAttribute("ParameterAttribute").ToList();
            ImplicitProperties = CmdletInfo.GeneratedProperties;
        }

        private static INamedTypeSymbol GetBaseClass(GeneratorExecutionContext context, INamedTypeSymbol type)
        {
            var genericTypeArg = type.GetAttributeConstructorValue<INamedTypeSymbol>("CmdletControllerAttribute");
            var isGeneric = genericTypeArg != null;
            var customBaseClass = type.GetAttributeNamedValue<INamedTypeSymbol>("CmdletControllerAttribute", "CustomBaseClass");

            if (customBaseClass != null)
            {
                Logger.Log($" - Custom base class: {customBaseClass}");

                return customBaseClass;
            };

            return isGeneric ? ControllerGenerator.ControllerBaseT : ControllerGenerator.ControllerBase;
        }

        private static string GetImportingConstructorBody(INamedTypeSymbol type)
            => string.Join("\n", type.GetPropertiesWithAttribute("ImportAttribute")
                .Select(p => $"            {p.Name} = {p.Name[0].ToString().ToLower()}{p.Name.Substring(1)};"));
    }
}
