//HintName: TfsCmdlets.Cmdlets.Git.Item.GetGitItem.g.cs
namespace TfsCmdlets.Cmdlets.Git.Item
{
    [Cmdlet("Get", "TfsGitItem", DefaultParameterSetName = "Get by commit SHA")]
    [OutputType(typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitItem))]
    public partial class GetGitItem: CmdletBase
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