//HintName: TfsCmdlets.Cmdlets.TeamProject.UndoTeamProjectRemoval.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet("Undo", "TfsTeamProjectRemoval", SupportsShouldProcess = true)]
    public partial class UndoTeamProjectRemoval: CmdletBase
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