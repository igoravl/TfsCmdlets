//HintName: TfsCmdlets.Cmdlets.TestManagement.RemoveTestPlan.g.cs
namespace TfsCmdlets.Cmdlets.TestManagement
{
    [Cmdlet("Remove", "TfsTestPlan", SupportsShouldProcess = true)]
    public partial class RemoveTestPlan: CmdletBase
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