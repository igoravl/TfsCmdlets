//HintName: TfsCmdlets.Cmdlets.Team.TeamAdmin.GetTeamAdmin.g.cs
namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    [Cmdlet("Get", "TfsTeamAdmin")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public partial class GetTeamAdmin: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
        public object Team { get; set; }
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