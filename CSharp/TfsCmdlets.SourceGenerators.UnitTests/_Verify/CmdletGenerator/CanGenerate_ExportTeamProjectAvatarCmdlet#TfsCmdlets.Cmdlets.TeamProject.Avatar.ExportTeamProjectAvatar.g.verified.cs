//HintName: TfsCmdlets.Cmdlets.TeamProject.Avatar.ExportTeamProjectAvatar.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    [Cmdlet("Export", "TfsTeamProjectAvatar", SupportsShouldProcess = true)]
    public partial class ExportTeamProjectAvatar: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
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