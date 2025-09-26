//HintName: TfsCmdlets.Cmdlets.TeamProject.SetTeamProject.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet("Set", "TfsTeamProject", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject))]
    public partial class SetTeamProject: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
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