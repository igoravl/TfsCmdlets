//HintName: TfsCmdlets.Cmdlets.TestManagement.RenameTestPlan.g.cs
namespace TfsCmdlets.Cmdlets.TestManagement
{
    [Cmdlet("Rename", "TfsTestPlan", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan))]
    public partial class RenameTestPlan: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_NEWNAME
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public string NewName { get; set; }
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