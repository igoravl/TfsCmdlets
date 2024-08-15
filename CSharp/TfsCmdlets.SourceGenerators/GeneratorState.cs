using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    public class GeneratorState
    {
        private Dictionary<string, object> PropertyBag { get; } = new Dictionary<string, object>();

        public GeneratorState(INamedTypeSymbol targetType, Logger logger)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            Name = targetType.Name;
            Namespace = targetType.FullNamespace();
            FullName = FileName = targetType.FullName();
            Logger = logger;
            TargetType = targetType;
        }

        public INamedTypeSymbol TargetType { get; set; }

        public string Name { get; }

        public string Namespace { get; }

        public string FullName { get; }

        public string FileName { get; }
        
        public IDictionary<string, GeneratedProperty> GeneratedProperties = new Dictionary<string, GeneratedProperty>();

        protected Logger Logger { get; }

        public object this[string key]
        {
            get => PropertyBag[key];
            set => PropertyBag[key] = value;
        }
    }
}
