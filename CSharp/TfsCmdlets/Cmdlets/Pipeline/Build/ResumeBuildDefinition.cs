using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Build
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(BuildDefinitionReference))]
    partial class ResumeBuildDefinition
    {
        /// <summary>
        /// Specifies the pipeline path.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        public object Definition { get; set; }
    }
}