using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    public interface IGenerator
    {
        void Initialize(GeneratorExecutionContext context);
        GeneratorState ProcessType(GeneratorExecutionContext context, INamedTypeSymbol type);
        string Generate(GeneratorState state);
    }
}