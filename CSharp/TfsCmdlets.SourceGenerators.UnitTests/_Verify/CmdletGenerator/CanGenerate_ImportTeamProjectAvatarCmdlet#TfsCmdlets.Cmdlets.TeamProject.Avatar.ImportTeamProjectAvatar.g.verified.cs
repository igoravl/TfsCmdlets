//HintName: TfsCmdlets.Cmdlets.TeamProject.Avatar.ImportTeamProjectAvatar.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    [Cmdlet("Import", "TfsTeamProjectAvatar", SupportsShouldProcess = true)]
    public partial class ImportTeamProjectAvatar: CmdletBase
    {
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