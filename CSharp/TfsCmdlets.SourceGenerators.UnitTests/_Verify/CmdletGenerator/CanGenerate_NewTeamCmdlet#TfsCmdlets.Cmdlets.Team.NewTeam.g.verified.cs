//HintName: TfsCmdlets.Cmdlets.Team.NewTeam.g.cs
namespace TfsCmdlets.Cmdlets.Team
{
    [Cmdlet("New", "TfsTeam", SupportsShouldProcess = true)]
    [OutputType(typeof(TfsCmdlets.Models.Team))]
    public partial class NewTeam: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
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