using System.Management.Automation;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Gets information from one or more process templates.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsProcessTemplate")]
    [OutputType(typeof(WebApiProcess))]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetProcessTemplate 
    {
        /// <summary>
        /// Specifies the name of the process template(s) to be returned. Wildcards are supported. 
        /// When omitted, all process templates in the given project collection are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by name")]
        [Alias("Name")]
        [SupportsWildcards()]
        public object ProcessTemplate { get; set; } = "*";

        /// <summary>
        /// Returns the default process template in the given orgnization / project collection.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get default process")]
        public SwitchParameter Default { get; set; }
    }
}