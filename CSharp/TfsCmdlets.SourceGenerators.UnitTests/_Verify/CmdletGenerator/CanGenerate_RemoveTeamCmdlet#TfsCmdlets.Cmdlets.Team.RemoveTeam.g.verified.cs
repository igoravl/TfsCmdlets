//HintName: TfsCmdlets.Cmdlets.Team.RemoveTeam.g.cs
namespace TfsCmdlets.Cmdlets.Team
{
    [Cmdlet("Remove", "TfsTeam", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.WebApiTeam))]
    public partial class RemoveTeam: CmdletBase
    {
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