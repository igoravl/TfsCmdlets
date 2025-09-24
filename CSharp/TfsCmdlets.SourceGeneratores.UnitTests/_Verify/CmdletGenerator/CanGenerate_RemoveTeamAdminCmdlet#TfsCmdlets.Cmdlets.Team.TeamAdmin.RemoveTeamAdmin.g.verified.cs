//HintName: TfsCmdlets.Cmdlets.Team.TeamAdmin.RemoveTeamAdmin.g.cs
namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    [Cmdlet("Remove", "TfsTeamAdmin", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public partial class RemoveTeamAdmin: CmdletBase
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