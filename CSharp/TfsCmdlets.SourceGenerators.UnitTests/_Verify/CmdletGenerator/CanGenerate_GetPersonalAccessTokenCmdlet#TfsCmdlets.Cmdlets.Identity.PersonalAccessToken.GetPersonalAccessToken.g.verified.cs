//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.GetPersonalAccessToken.g.cs
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    [Cmdlet("Get", "TfsPersonalAccessToken", DefaultParameterSetName = "Get by name")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken))]
    public partial class GetPersonalAccessToken: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}