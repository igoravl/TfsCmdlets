//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.RenamePersonalAccessToken.g.cs
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    [Cmdlet("Rename", "TfsPersonalAccessToken", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken))]
    public partial class RenamePersonalAccessToken: CmdletBase
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