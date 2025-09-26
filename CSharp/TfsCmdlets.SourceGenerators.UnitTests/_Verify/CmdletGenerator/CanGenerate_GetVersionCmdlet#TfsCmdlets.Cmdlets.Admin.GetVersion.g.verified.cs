//HintName: TfsCmdlets.Cmdlets.Admin.GetVersion.g.cs
namespace TfsCmdlets.Cmdlets.Admin
{
    [Cmdlet("Get", "TfsVersion")]
    [OutputType(typeof(TfsCmdlets.Models.ServerVersion))]
    public partial class GetVersion: CmdletBase
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