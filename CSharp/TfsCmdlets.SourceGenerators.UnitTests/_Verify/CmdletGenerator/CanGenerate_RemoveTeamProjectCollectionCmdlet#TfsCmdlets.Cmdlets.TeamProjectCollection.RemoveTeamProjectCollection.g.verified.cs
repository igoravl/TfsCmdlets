//HintName: TfsCmdlets.Cmdlets.TeamProjectCollection.RemoveTeamProjectCollection.g.cs
namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    [Cmdlet("Remove", "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    public partial class RemoveTeamProjectCollection: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}