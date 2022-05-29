using System.Management.Automation;
using Microsoft.Azure.Pipelines.WebApi;
using WebApiPipelineVariable = Microsoft.Azure.Pipelines.WebApi.Variable;
using WebApiVariableGroup = Microsoft.TeamFoundation.DistributedTask.WebApi.VariableGroup;

namespace TfsCmdlets.Cmdlets.Pipeline.Variables
{
    /// <summary>
    /// Gets one or more variables from a pipeline, a release or a variable group.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DataType = typeof(Models.Pipeline.VariableGroup))]
    [OutputType(typeof(WebApiPipelineVariable), ParameterSetName = new[]{"By Pipeline"})]
    [OutputType(typeof(WebApiVariableGroup), ParameterSetName = new[] { "By Variable Group" })]
    partial class GetVariable
    {
        /// <summary>
        /// Specifies the name of the variable. Wildcards are supported. 
        /// When omitted, returns all variables in the specified context (pipeline, release or variable group).
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        public object Variable { get; set; } = "*";

        /// <summary>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "By Pipeline")]
        public object Pipeline { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "By Release")]
        public object Release { get; set; }

        /// <summary>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "By Variable Group")]
        public object VariableGroup { get; set; }
    }
}