//HintName: TfsCmdlets.Cmdlets.TeamProject.NewTeamProject.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet("New", "TfsTeamProject", SupportsShouldProcess = true, DefaultParameterSetName = "Get by project")]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject))]
    public partial class NewTeamProject: CmdletBase
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