//HintName: TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.NewReleaseDefinitionFolder.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    [Cmdlet("New", "TfsReleaseDefinitionFolder", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder))]
    public partial class NewReleaseDefinitionFolder: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }
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