//HintName: TfsCmdlets.Cmdlets.TestManagement.GetTestPlan.g.cs
namespace TfsCmdlets.Cmdlets.TestManagement
{
    [Cmdlet("Get", "TfsTestPlan")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan))]
    public partial class GetTestPlan: CmdletBase
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