//HintName: TfsCmdlets.Cmdlets.ExtensionManagement.GetExtension.g.cs
namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    [Cmdlet("Get", "TfsExtension")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension))]
    public partial class GetExtension: CmdletBase
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