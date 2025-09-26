//HintName: TfsCmdlets.Cmdlets.Identity.Group.NewGroup.g.cs
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    [Cmdlet("New", "TfsGroup", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Graph.Client.GraphGroup))]
    public partial class NewGroup: CmdletBase
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