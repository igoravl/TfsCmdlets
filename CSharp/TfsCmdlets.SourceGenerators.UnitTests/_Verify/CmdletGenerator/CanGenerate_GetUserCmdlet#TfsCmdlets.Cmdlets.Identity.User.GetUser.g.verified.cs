//HintName: TfsCmdlets.Cmdlets.Identity.User.GetUser.g.cs
namespace TfsCmdlets.Cmdlets.Identity.User
{
    [Cmdlet("Get", "TfsUser", DefaultParameterSetName = "Get by ID or Name")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Licensing.AccountEntitlement))]
    public partial class GetUser: CmdletBase
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