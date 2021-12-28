using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Creates a new team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(Models.Team))]
    partial class NewTeam
    {
        /// <summary>
        /// Specifies the name of the new team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Team { get; set; }

        /// <summary>
        /// Specifies the team's default area path (or "team field"). The default area path is assigned
        /// automatically to all work items created in a team's backlog and/or board. When omitted,
        /// an area path may still be associated to this team depending on whether NoAutomaticAreaPath
        /// is set
        /// </summary>
        [Parameter]
        [Alias("TeamFieldValue")]
        public string DefaultAreaPath { get; set; }

        /// <summary>
        /// Do not associate an area path automatically to the new team. When omitted, an area path 
        /// is created (if needed) and then is set as the default area path / team field
        /// </summary>
        [Parameter]
        public SwitchParameter NoDefaultArea { get; set; }

        /// <summary>
        /// Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration.
        /// </summary>
        [Parameter]
        public string BacklogIteration { get; set; } = "\\";

        /// <summary>
        /// Specifies the backlog iteration paths that are associated with this team. Provide a list 
        /// of iteration paths in the form '/path1/path2'.
        /// </summary>
        [Parameter]
        public object IterationPaths { get; set; }

        /// <summary>
        /// Specifies the default iteration macro. When omitted, defaults to "@CurrentIteration".
        /// </summary>
        [Parameter]
        public string DefaultIterationMacro { get; set; } = "@CurrentIteration";

        /// <summary>
        /// Do not associate an iteration path automatically to the new team. When omitted, 
        /// an iteration path is created (if needed) and then is set as the default 
        /// backlog iteration
        /// </summary>
        [Parameter]
        public SwitchParameter NoBacklogIteration { get; set; }

        /// <summary>
        /// Specifies a description of the new team.
        /// </summary>
        [Parameter]
        public string Description { get; set; }
    }
}