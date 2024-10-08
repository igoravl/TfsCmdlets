﻿using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public class Filter : BaseFilter
    {
        public override bool ShouldProcessType(INamedTypeSymbol type) => type.HasAttribute("HttpClientAttribute");
    }
}