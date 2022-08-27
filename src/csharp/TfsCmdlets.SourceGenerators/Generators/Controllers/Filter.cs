using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    public class Filter : BaseFilter
    {
        public override bool ShouldProcessType(INamedTypeSymbol type) => type.HasAttribute("CmdletControllerAttribute");
    }
}