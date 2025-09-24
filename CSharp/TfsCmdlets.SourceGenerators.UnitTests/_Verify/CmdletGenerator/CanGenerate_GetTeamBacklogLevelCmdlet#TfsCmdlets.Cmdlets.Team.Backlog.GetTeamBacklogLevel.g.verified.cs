//HintName: TfsCmdlets.Cmdlets.Team.Backlog.GetTeamBacklogLevel.g.cs
namespace TfsCmdlets.Cmdlets.Team.Backlog
{
    [Cmdlet("Get", "TfsTeamBacklogLevel")]
    [OutputType(typeof(Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration))]
    public partial class GetTeamBacklogLevel: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
        public object Team { get; set; }
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter()]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}