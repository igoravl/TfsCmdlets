//HintName: TfsCmdlets.Cmdlets.ExtensionManagement.EnableExtension.g.cs
namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    [Cmdlet("Enable", "TfsExtension", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension))]
    public partial class EnableExtension: CmdletBase
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