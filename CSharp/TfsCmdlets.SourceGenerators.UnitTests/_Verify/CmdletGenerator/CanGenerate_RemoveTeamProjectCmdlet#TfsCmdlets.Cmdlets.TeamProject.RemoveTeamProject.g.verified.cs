//HintName: TfsCmdlets.Cmdlets.TeamProject.RemoveTeamProject.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet("Remove", "TfsTeamProject", SupportsShouldProcess = true)]
    public partial class RemoveTeamProject: CmdletBase
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