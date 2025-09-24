//HintName: TfsCmdlets.Cmdlets.ProcessTemplate.NewProcessTemplate.g.cs
namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    [Cmdlet("New", "TfsProcessTemplate", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.Process))]
    public partial class NewProcessTemplate: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
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