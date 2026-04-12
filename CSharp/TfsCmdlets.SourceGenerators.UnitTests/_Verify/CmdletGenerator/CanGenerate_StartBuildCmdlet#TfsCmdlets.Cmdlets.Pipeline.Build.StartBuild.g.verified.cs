//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.StartBuild.g.cs
namespace TfsCmdlets.Cmdlets.Pipeline.Build
{
    [Cmdlet("Start", "TfsBuild", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference))]
    public partial class StartBuild: CmdletBase
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