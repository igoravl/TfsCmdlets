using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Controllers
{
    [Generator]
    public class CmdletGenerator : BaseGenerator<Filter, TypeProcessor>
    {
        protected override string GeneratorName => "ControllerGenerator";
    }
}