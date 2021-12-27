using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

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
        /// Specifies the backlog area paths that are associated with this team. Provide a list 
        /// of area paths in the form '/path1/path2/[*]'. When the path ends with an asterisk, all
        /// child area path will be included recursively. Otherwise, only the area itself (without 
        /// its children) will be included.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string[] AreaPaths { get; set; }

        /// <summary>
        /// Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string BacklogIteration { get; set; } = "\\";

        /// <summary>
        /// Specifies the backlog iteration paths that are associated with this team. Provide a list 
        /// of iteration paths in the form '/path1/path2'.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string[] IterationPaths { get; set; }

        /// <summary>
        /// Specifies the default iteration macro. When omitted, defaults to "@CurrentIteration".
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string DefaultIterationMacro { get; set; } = "@CurrentIteration";

        /// <summary>
        ///  Specifies the team's Working Days. When omitted, defaults to Monday thru Friday
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public IEnumerable<string> WorkingDays = new[] { "monday", "tuesday", "wednesday", "thursday", "friday" };

        /// <summary>
        /// Specifies how bugs should behave when added to a board.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        [ValidateSet("AsTasks", "AsRequirements", "Off")]
        public string BugsBehavior { get; set; }

        /// <summary>
        /// Specifies which backlog levels (e.g. Epics, Features, Stories) should be visible.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public Hashtable BacklogVisibilities { get; set; }
    }
}