//HintName: TfsCmdlets.Cmdlets.TeamProjectCollection.NewTeamProjectCollection.g.cs
namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    [Cmdlet("New", "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    [OutputType(typeof(TfsCmdlets.Models.Connection))]
    public partial class NewTeamProjectCollection: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}