//HintName: TfsCmdlets.Cmdlets.Identity.GetIdentity.g.cs
namespace TfsCmdlets.Cmdlets.Identity
{
    [Cmdlet("Get", "TfsIdentity")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public partial class GetIdentity: CmdletBase
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