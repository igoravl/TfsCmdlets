//HintName: TfsCmdlets.Cmdlets.ExtensionManagement.DisableExtension.g.cs
namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    [Cmdlet("Disable", "TfsExtension", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension))]
    public partial class DisableExtension: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
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