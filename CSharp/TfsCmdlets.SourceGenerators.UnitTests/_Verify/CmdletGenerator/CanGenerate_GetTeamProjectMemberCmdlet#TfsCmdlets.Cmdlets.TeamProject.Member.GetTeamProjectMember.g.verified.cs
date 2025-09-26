//HintName: TfsCmdlets.Cmdlets.TeamProject.Member.GetTeamProjectMember.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject.Member
{
    [Cmdlet("Get", "TfsTeamProjectMember")]
    [OutputType(typeof(TfsCmdlets.Models.TeamProjectMember))]
    public partial class GetTeamProjectMember: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
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