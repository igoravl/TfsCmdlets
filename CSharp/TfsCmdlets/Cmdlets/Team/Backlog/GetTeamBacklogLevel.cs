using System.Management.Automation;
using WebApiBacklogLevelConfiguration = Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration;

namespace TfsCmdlets.Cmdlets.Team.Backlog
{
    /// <summary>
    /// Gets information about one or more backlog levels of a given team.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBacklogLevel")]
    [OutputType(typeof(WebApiBacklogLevelConfiguration))]
    [TfsCmdlet(CmdletScope.Team)]
    partial class GetTeamBacklogLevel
    {
        /// <summary>
        /// Specifies one or more backlog level configurations to be returned. Valid values 
        /// are the name (e.g. "Stories") or the ID (e.g. "Microsoft.RequirementCategory") of the 
        /// backlog level to return. Wilcards are supported. When omitted, returns all backlogs 
        /// levels of the given team.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Backlog { get; set; } = "*";
    }
}
