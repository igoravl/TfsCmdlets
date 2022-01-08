using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    public class GeneratorState
    {
        private Dictionary<string, object> PropertyBag { get; } = new Dictionary<string, object>();

        public GeneratorState(INamedTypeSymbol type)
        {
            Name = type.Name;
            Namespace = type.FullNamespace();
            FullName = FileName = type.FullName();
        }

        public string Name { get;  }

        public string Namespace{ get; }

        public string FullName { get; }

        public string FileName { get; }

        public IDictionary<string, GeneratedProperty> GeneratedProperties = new Dictionary<string, GeneratedProperty>();

        public object this[string key]
        {
            get => PropertyBag[key];
            set => PropertyBag[key] = value;
        }
    }
}
