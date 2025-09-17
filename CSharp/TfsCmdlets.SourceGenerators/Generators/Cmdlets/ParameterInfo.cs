using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    public record ParameterInfo : PropertyInfo
    {
        public ParameterInfo(IPropertySymbol prop)
            : base(prop)
        {
            var attr = prop.GetAttribute("Parameter"); // TODO
            if (attr == null) throw new ArgumentException($"Property {prop.Name} is not a Cmdlet parameter");

            DontShow = attr.GetAttributeNamedValue<bool>("DontShow");
            HelpMessage = attr.GetAttributeNamedValue<string>("HelpMessage");
            HelpMessageBaseName = attr.GetAttributeNamedValue<string>("HelpMessageBaseName");
            HelpMessageResourceId = attr.GetAttributeNamedValue<string>("HelpMessageResourceId");
            Mandatory = attr.GetAttributeNamedValue<bool>("Mandatory");
            ParameterSetName = attr.GetAttributeNamedValue<string>("ParameterSetName") ?? "__AllParameterSets";
            Position = attr.GetAttributeNamedValue<int>("Position");
            ValueFromPipeline = attr.GetAttributeNamedValue<bool>("ValueFromPipeline");
            ValueFromPipelineByPropertyName = attr.GetAttributeNamedValue<bool>("ValueFromPipelineByPropertyName");
            ValueFromRemainingArguments = attr.GetAttributeNamedValue<bool>("ValueFromRemainingArguments");
        }

        public bool DontShow { get; }

        public string HelpMessage { get; }

        public string HelpMessageBaseName { get; }

        public string HelpMessageResourceId { get; }

        public bool Mandatory { get; }

        public string ParameterSetName { get; }

        public int Position { get; }

        public bool ValueFromPipeline { get; }

        public bool ValueFromPipelineByPropertyName { get; }

        public bool ValueFromRemainingArguments { get; }
    }
}
