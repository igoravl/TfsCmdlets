using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Models
{
    [Generator]
    public class ModelGenerator : BaseGenerator<Filter, TypeProcessor>
    {
        protected override string GeneratorName => nameof(ModelGenerator);
    }
}