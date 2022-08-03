using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Gets information from one or more process templates.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiProcess))]
    partial class GetProcessTemplate 
    {
        /// <summary>
        /// Specifies the name of the process template(s) to be returned. Wildcards are supported. 
        /// When omitted, all process templates in the given project collection are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by name")]
        [Alias("Name", "Process")]
        [SupportsWildcards()]
        public object ProcessTemplate { get; set; } = "*";

        /// <summary>
        /// Returns the default process template in the given orgnization / project collection.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get default process")]
        public SwitchParameter Default { get; set; }
    }
}