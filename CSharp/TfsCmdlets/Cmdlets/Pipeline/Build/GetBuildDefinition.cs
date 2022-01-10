using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Build
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(BuildDefinitionReference))]
    partial class GetBuildDefinition
    {
        /// <summary>
        /// Specifies the pipeline path. Wildcards are supported. 
        /// When omitted, all pipelines definitions in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Definition { get; set; } = "\\**";

        /// <summary>
        /// Specifies the query order. When omitted, defaults to None.
        /// </summary>
        [Parameter]
        public DefinitionQueryOrder QueryOrder { get; set; }
    }
}