//HintName: TfsCmdlets.Cmdlets.Team.TeamMember.RemoveTeamMember.g.cs
namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    [Cmdlet("Remove", "TfsTeamMember", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public partial class RemoveTeamMember: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter()]
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