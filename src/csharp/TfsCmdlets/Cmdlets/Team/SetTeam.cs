using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Changes the details of a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTeam))]
    partial class SetTeam
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Set team settings")]
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Set default team")]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Team { get; set; }

        /// <summary>
        /// Sets the specified team as the default team.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Set default team")]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// Specifies a new description
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the team's default area path (or "team field"). The default area path is assigned
        /// automatically to all work items created in a team's backlog and/or board.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        [Alias("TeamFieldValue")]
        public string DefaultAreaPath { get; set; }

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
        /// Replaces the existing area paths with the specified list of area paths. 
        /// When omitted, the new area paths are added alongside the previously defined ones.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public SwitchParameter OverwriteAreaPaths { get; set; }

        /// <summary>
        /// Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string BacklogIteration { get; set; } = "\\";

        /// <summary>
        /// Specifies the default iteration macro. 
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string DefaultIterationMacro { get; set; }

        /// <summary>
        /// Specifies the backlog iteration path(s) that are associated with this team. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string[] IterationPaths { get; set; }

        /// <summary>
        /// Replaces the existing iteration paths with the specified list of iteration paths. 
        /// When omitted, the new iteration paths are added alongside the previously defined ones.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public SwitchParameter OverwriteIterationPaths { get; set; }

        /// <summary>
        ///  Specifies the team's Working Days.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public DayOfWeek[] WorkingDays { get; set; }

        /// <summary>
        /// Specifies how bugs should behave when added to a board.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public BugsBehavior BugsBehavior { get; set; }

        /// <summary>
        /// Specifies which backlog levels (e.g. Epics, Features, Stories) should be visible.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public Hashtable BacklogVisibilities { get; set; }

        /// <summary>
        /// Allows the cmdlet to create target area and/or iteration nodes if they're missing.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public SwitchParameter Force { get; set; }
    }
}