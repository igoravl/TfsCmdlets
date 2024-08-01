using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    [Generator]
    public class HttpClientGenerator : BaseGenerator<Filter, TypeProcessor>
    {
        protected override string GeneratorName => nameof(HttpClientGenerator);
    }
}