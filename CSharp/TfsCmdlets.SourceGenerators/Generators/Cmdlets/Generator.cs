using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Cmdlets
{
    [Generator]
    public class CmdletGenerator : BaseGenerator<Filter, TypeProcessor>
    {
    }
}