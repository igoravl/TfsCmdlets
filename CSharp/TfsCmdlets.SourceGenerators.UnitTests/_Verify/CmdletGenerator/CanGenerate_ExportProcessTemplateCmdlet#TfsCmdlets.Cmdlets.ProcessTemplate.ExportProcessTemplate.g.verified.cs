//HintName: TfsCmdlets.Cmdlets.ProcessTemplate.ExportProcessTemplate.g.cs
namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    [Cmdlet("Export", "TfsProcessTemplate", SupportsShouldProcess = true)]
    public partial class ExportProcessTemplate: CmdletBase
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