//HintName: TfsCmdlets.Cmdlets.ProcessTemplate.ImportProcessTemplate.g.cs
namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    [Cmdlet("Import", "TfsProcessTemplate", SupportsShouldProcess = true)]
    public partial class ImportProcessTemplate: CmdletBase
    {
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