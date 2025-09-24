//HintName: TfsCmdlets.Cmdlets.TestManagement.NewTestPlan.g.cs
namespace TfsCmdlets.Cmdlets.TestManagement
{
    [Cmdlet("New", "TfsTestPlan", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan))]
    public partial class NewTestPlan: CmdletBase
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