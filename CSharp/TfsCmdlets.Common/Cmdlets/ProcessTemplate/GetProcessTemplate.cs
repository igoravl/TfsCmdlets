using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Gets information from one or more XML-based process templates.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsProcessTemplate")]
    [DesktopOnly]
    public partial class GetProcessTemplate: BaseCmdlet
    {
        /// <summary>
        /// Specifies the name of the process template(s) to be returned. Wildcards supported. 
        /// When omitted, all process templates in the given project collection are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string ProcessTemplate { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Collection { get; set; }
    }
}