//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.RemovePersonalAccessToken.g.cs
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    [Cmdlet("Remove", "TfsPersonalAccessToken", SupportsShouldProcess = true, DefaultParameterSetName = "Remove own token")]
    public partial class RemovePersonalAccessToken: CmdletBase
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