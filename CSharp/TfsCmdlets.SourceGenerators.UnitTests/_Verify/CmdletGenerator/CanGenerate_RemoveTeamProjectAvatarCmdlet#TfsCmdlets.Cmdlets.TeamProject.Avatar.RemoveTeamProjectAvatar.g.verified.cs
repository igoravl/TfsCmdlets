//HintName: TfsCmdlets.Cmdlets.TeamProject.Avatar.RemoveTeamProjectAvatar.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    [Cmdlet("Remove", "TfsTeamProjectAvatar", SupportsShouldProcess = true)]
    public partial class RemoveTeamProjectAvatar: CmdletBase
    {
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