//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Folder.RemoveBuildDefinitionFolder.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    [Cmdlet("Remove", "TfsBuildDefinitionFolder", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Build.WebApi.Folder))]
    public partial class RemoveBuildDefinitionFolder: CmdletBase
    {
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