using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    [Generator]
    public class ControllerGenerator : BaseGenerator<Filter, TypeProcessor>
    {
        protected override string GeneratorName => nameof(ControllerGenerator);
    }
}