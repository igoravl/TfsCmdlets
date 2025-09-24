//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Definition.GetBuildDefinition.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Definition
{
    [Cmdlet("Get", "TfsBuildDefinition")]
    [OutputType(typeof(Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference))]
    public partial class GetBuildDefinition: CmdletBase
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