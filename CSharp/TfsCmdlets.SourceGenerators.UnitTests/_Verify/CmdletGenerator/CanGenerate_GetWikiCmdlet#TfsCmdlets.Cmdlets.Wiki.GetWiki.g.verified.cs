//HintName: TfsCmdlets.Cmdlets.Wiki.GetWiki.g.cs
namespace TfsCmdlets.Cmdlets.Wiki
{
    [Cmdlet("Get", "TfsWiki", DefaultParameterSetName = "Get all wikis")]
    [OutputType(typeof(Microsoft.TeamFoundation.Wiki.WebApi.WikiV2))]
    public partial class GetWiki: CmdletBase
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