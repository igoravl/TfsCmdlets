using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Models
{
    public class Filter : BaseFilter
    {
        public override bool ShouldProcessType(INamedTypeSymbol type) => type.HasAttribute("ModelAttribute");
    }
}