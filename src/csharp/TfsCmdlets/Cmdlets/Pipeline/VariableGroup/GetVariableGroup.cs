using System.Management.Automation;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using WebApiVariableGroup = Microsoft.TeamFoundation.DistributedTask.WebApi.VariableGroup;

namespace TfsCmdlets.Cmdlets.Pipeline.VariableGroup
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DataType = typeof(Models.Pipeline.VariableGroup), OutputType = typeof(WebApiVariableGroup))]
    partial class GetVariableGroup
    {
        /// <summary>
        /// Specifies the name or ID of the variable group. Wildcards are supported. 
        /// When omitted, returns all variable groups in the supplied team project.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        public object VariableGroup { get; set; } = "*";

        // /// <summary>
        // /// Includes details about the variables groups, such as the team project they're associated with.
        // /// Specifying this argument increases the time it takes to complete the operation.
        // /// </summary>
        // [Parameter]
        // public SwitchParameter IncludeDetails { get; set; }
    }
}