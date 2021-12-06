﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace TfsCmdlets.SourceGenerators
{
    [Generator]
    public class CmdletGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new CmdletSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is CmdletSyntaxReceiver syntaxReceiver)) return;

            //if(!Debugger.IsAttached) Debugger.Launch();

            foreach (var cmdlet in syntaxReceiver.Cmdlets)
            {
                var settings = GetSettings(cmdlet);
                var props = new StringBuilder();

                // Add verb-specific properties

                switch (settings.Verb)
                {
                    case "Rename":
                        {
                            props.Append(@"
        /// <summary>
        /// HELP_PARAM_NEWNAME
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public string NewName { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter]
        public SwitchParameter Passthru { get; set; }
");
                            break;
                        }
                    case "New":
                    case "Set":
                        {
                            props.Append(@"
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter]
        public SwitchParameter Passthru { get; set; }
");
                            break;
                        }
                }

                // Add scope-specific properties

                props.Append(GetPropertyFor(cmdlet, CmdletScope.Team));
                props.Append(GetPropertyFor(cmdlet, CmdletScope.Project));
                props.Append(GetPropertyFor(cmdlet, CmdletScope.Collection));
                props.Append(GetPropertyFor(cmdlet, CmdletScope.Server));

                context.AddSource($"{cmdlet.FullName()}.cs", SourceText.From($@"/*
Generated by: [TfsCmdlets.SourceGenerators.CmdletGenerator]
Class name  : {cmdlet.FullName()}
Verb        : {settings.Verb}
SkipCodeGen : {settings.SkipAutoProperties}
*/

using System.Management.Automation;
using System.Composition;
using TfsCmdlets.Services;

// ReSharper disable once CheckNamespace
namespace {cmdlet.FullNamespace()}
{{
  public partial class {cmdlet.Name}: CmdletBase
  {{
{(settings.SkipAutoProperties ? string.Empty : props.ToString())}
  }}
}}
",
                    Encoding.UTF8));
            }
        }

        private CmdletSettings GetSettings(INamedTypeSymbol cmdlet)
        {
            return new CmdletSettings
            {
                Verb = GetVerb(cmdlet),
                Scope = GetScope(cmdlet),
                SkipAutoProperties = GetSkipAutoProperties(cmdlet),
                DesktopOnly = GetDesktopOnly(cmdlet),
                HostedOnly = GetHostedOnly(cmdlet),
                RequiresVersion = GetRequiresVersion(cmdlet)
            };
        }

        private string GetVerb(INamedTypeSymbol cmdlet)
            => GetAttributeConstructorValue<string>(cmdlet, "CmdletAttribute");

        private CmdletScope GetScope(INamedTypeSymbol cmdlet)
            => GetAttributeConstructorValue<CmdletScope>(cmdlet, "TfsCmdletAttribute");

        private bool GetSkipAutoProperties(INamedTypeSymbol cmdlet)
            => GetAttributeNamedValue(cmdlet, "TfsCmdletAttribute", "SkipAutoProperties");

        private bool GetDesktopOnly(INamedTypeSymbol cmdlet)
            => GetAttributeNamedValue(cmdlet, "TfsCmdletAttribute", "DesktopOnly");

        private bool GetHostedOnly(INamedTypeSymbol cmdlet)
            => GetAttributeNamedValue(cmdlet, "TfsCmdletAttribute", "HostedOnly");

        private int GetRequiresVersion(INamedTypeSymbol cmdlet)
            => GetAttributeNamedValue<int>(cmdlet, "TfsCmdletAttribute", "RequiresVersion");

        private string GetPropertyFor(ISymbol cmdlet, CmdletScope currentScope)
        {
            int.TryParse(cmdlet
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals("TfsCmdletAttribute"))?
                .ConstructorArguments[0].Value.ToString(), out var cmdletScope);

            if (cmdletScope < (int)currentScope) return string.Empty;

            var scopeName = currentScope.ToString();
            var isPipeline = cmdlet.Name.StartsWith("Get") && cmdletScope == (int)currentScope;
            var valueFromPipeline = isPipeline ? "(ValueFromPipeline=true)" : string.Empty;

            return $@"
        /// <summary>
        /// HELP_PARAM_{scopeName.ToUpper()}
        /// </summary>
        [Parameter{valueFromPipeline}]
        public object {scopeName} {{ get; set; }}
";
        }

        private T GetAttributeConstructorValue<T>(INamedTypeSymbol cmdlet, string attributeName, int argumentPosition = 0)
        {
            var attr = cmdlet
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals(attributeName));

            if (attr == null) return default(T);

            var arg = attr.ConstructorArguments[argumentPosition];

            return (T) arg.Value;
        }

        private T GetAttributeNamedValue<T>(INamedTypeSymbol cmdlet, string attributeName, string argumentName)
        {
            var attr = cmdlet
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals(attributeName));

            if (attr == null) return default(T);

            var arg = attr.NamedArguments.FirstOrDefault(a => a.Key.Equals(argumentName));

            return (T) (arg.Value.Value ?? default(T));
        }

        private bool GetAttributeNamedValue(INamedTypeSymbol cmdlet, string attributeName, string argumentName, bool defaultValue = false)
        {
            var attr = cmdlet
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals(attributeName));

            if (attr == null) return defaultValue;

            var arg = attr.NamedArguments.FirstOrDefault(a => a.Key.Equals(argumentName));

            return (arg.Value.Value?.ToString() ?? string.Empty).Equals("True", StringComparison.OrdinalIgnoreCase);
        }

        internal class CmdletSyntaxReceiver : ISyntaxContextReceiver
        {
            internal List<INamedTypeSymbol> Cmdlets { get; } = new List<INamedTypeSymbol>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                var node = context.Node;

                if (!(node is ClassDeclarationSyntax cds)) return;

                var type = context.SemanticModel.GetDeclaredSymbol(cds) as INamedTypeSymbol;

                if (!type.GetAttributes().Any(attr1 => attr1.AttributeClass.Name.Equals("CmdletAttribute"))) return;

                Cmdlets.Add(type);
            }
        }

        [Flags]
        private enum CmdletScope
        {
            None = 0,
            Server = 1,
            Collection = 2,
            Project = 3,
            Team = 4
        }

        private class CmdletSettings
        {
            internal string Verb { get; set; }
            internal CmdletScope Scope { get; set; }
            internal bool SkipAutoProperties { get; set; }
            internal bool DesktopOnly { get; set; }
            internal bool HostedOnly { get; set; }
            internal int RequiresVersion { get; set; }
        }

    }
}