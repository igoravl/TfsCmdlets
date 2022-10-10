using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// Starts an offline team project collection and make it online.
    /// </summary>
    [TfsCmdlet(CmdletScope.Server, SupportsShouldProcess = true)]
    partial class StopTeamProjectCollection
    {
        /// <summary>
        /// Specifies the name of the collection to start.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Collection { get; set; }
    }
}
