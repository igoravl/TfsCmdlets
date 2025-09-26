//HintName: TfsCmdlets.Cmdlets.TeamProject.RenameTeamProject.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet("Rename", "TfsTeamProject", SupportsShouldProcess = true)]
    public partial class RenameTeamProject: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_NEWNAME
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public string NewName { get; set; }
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