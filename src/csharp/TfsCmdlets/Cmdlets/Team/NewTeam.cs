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
        /// Specifies the backlog area path(s) that are associated with this team. Wildcards are supported. 
        /// When the path ends with an asterisk, all child area paths will be included recursively. 
        /// Otherwise, only the area itself (without its children) will be included.
        /// To include the children of the default area path, use the wildcard character (*) without a path.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        [SupportsWildcards]
        public string[] AreaPaths { get; set; }

        /// <summary>
        /// Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration.
        /// </summary>
        [Parameter]
        public string BacklogIteration { get; set; } = "\\";

        /// <summary>
        /// Specifies the default iteration macro. When omitted, defaults to "@CurrentIteration".
        /// </summary>
        [Parameter]
        public string DefaultIterationMacro { get; set; } = "@CurrentIteration";

        /// <summary>
        /// Specifies the backlog iteration path(s) that are associated with this team. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string[] IterationPaths { get; set; }

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