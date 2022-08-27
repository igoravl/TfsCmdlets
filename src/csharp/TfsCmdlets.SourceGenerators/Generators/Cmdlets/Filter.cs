using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    public class Filter : BaseFilter
    {
        public override bool ShouldProcessType(INamedTypeSymbol type) => type.HasAttribute("TfsCmdletAttribute");
    }
}