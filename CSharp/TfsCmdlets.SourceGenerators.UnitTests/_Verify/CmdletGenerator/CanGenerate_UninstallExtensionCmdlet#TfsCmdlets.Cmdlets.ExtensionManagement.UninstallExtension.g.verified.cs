//HintName: TfsCmdlets.Cmdlets.ExtensionManagement.UninstallExtension.g.cs
namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    [Cmdlet("Uninstall", "TfsExtension", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension))]
    public partial class UninstallExtension: CmdletBase
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