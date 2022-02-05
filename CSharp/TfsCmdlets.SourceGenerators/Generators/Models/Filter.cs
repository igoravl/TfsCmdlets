using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Models
{
    public class Filter : BaseFilter
    {
        public override bool ShouldProcessType(INamedTypeSymbol type)
        {
            var baseClass = type.BaseType;

            return baseClass != null && baseClass.FullName().StartsWith("TfsCmdlets.Models.ModelBase");
        }
    }
}