//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Folder.NewBuildDefinitionFolder.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    [Cmdlet("New", "TfsBuildDefinitionFolder", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Build.WebApi.Folder))]
    public partial class NewBuildDefinitionFolder: CmdletBase
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