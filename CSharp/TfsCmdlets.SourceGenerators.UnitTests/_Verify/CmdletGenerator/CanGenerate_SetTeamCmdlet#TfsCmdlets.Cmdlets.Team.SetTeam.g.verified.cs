//HintName: TfsCmdlets.Cmdlets.Team.SetTeam.g.cs
namespace TfsCmdlets.Cmdlets.Team
{
    [Cmdlet("Set", "TfsTeam", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.WebApiTeam))]
    public partial class SetTeam: CmdletBase
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