//HintName: TfsCmdlets.Cmdlets.Wiki.RemoveWiki.g.cs
namespace TfsCmdlets.Cmdlets.Wiki
{
    [Cmdlet("Remove", "TfsWiki", SupportsShouldProcess = true, DefaultParameterSetName = "Remove code wiki")]
    public partial class RemoveWiki: CmdletBase
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