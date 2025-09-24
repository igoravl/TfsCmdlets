//HintName: TfsCmdlets.Cmdlets.Wiki.NewWiki.g.cs
namespace TfsCmdlets.Cmdlets.Wiki
{
    [Cmdlet("New", "TfsWiki", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Wiki.WebApi.WikiV2))]
    public partial class NewWiki: CmdletBase
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