//HintName: TfsCmdlets.Cmdlets.ExtensionManagement.InstallExtension.g.cs
namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    [Cmdlet("Install", "TfsExtension", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension))]
    public partial class InstallExtension: CmdletBase
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