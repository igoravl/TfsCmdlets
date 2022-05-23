using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    [Generator]
    public class CmdletGenerator : BaseGenerator<Filter, TypeProcessor>
    {
        protected override string GeneratorName => nameof(CmdletGenerator);
    }
}