//HintName: TfsCmdlets.Cmdlets.Identity.Group.GetGroupMember.g.cs
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    [Cmdlet("Get", "TfsGroupMember")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public partial class GetGroupMember: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}