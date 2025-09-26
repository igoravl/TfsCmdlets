using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Models
{
    public record ModelInfo: ClassInfo
    {
        public ModelInfo(INamedTypeSymbol namedTypeSymbol) : base(namedTypeSymbol) { }

        public string ModelType { get; set; }
        public string DataType { get; private set; }

        public static ModelInfo Create(GeneratorAttributeSyntaxContext ctx)
        {
            if (ctx.TargetSymbol is not INamedTypeSymbol symbol) return null;

            return new ModelInfo(symbol)
            {
                ModelType = symbol.FullName(),
                DataType = symbol.GetAttributeConstructorValue<INamedTypeSymbol>("ModelAttribute").FullName()
            };
        }
    }
}
