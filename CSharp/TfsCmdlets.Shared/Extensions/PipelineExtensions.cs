using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Extensions
{
    public static class PipelineExtensions
    {
        public static string GetFullPath(this BuildDefinitionReference buildDefinition) =>
            $"{buildDefinition.Path}\\{buildDefinition.Name}";
    }
}