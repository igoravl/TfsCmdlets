//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Folder.GetBuildDefinitionFolder.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    [Cmdlet("Get", "TfsBuildDefinitionFolder")]
    [OutputType(typeof(Microsoft.TeamFoundation.Build.WebApi.Folder))]
    public partial class GetBuildDefinitionFolder: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
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