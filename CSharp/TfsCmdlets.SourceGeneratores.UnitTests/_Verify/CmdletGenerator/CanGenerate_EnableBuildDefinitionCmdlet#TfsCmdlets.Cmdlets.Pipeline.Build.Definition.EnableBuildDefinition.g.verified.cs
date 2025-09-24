//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Definition.EnableBuildDefinition.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Definition
{
    [Cmdlet("Enable", "TfsBuildDefinition", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference))]
    public partial class EnableBuildDefinition: CmdletBase
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