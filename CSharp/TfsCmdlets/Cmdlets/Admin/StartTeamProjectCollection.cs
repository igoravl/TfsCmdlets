using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// Starts an offline team project collection and make it online.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    [TfsCmdlet(CmdletScope.None)]
    partial class StartTeamProjectCollection
    {
        /// <summary>
        /// Specifies the name of the collection to start.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string Collection { get; set; }
    }
}