using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// Starts an offline team project collection and make it online.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    [TfsCmdlet(CmdletScope.Server)]
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
