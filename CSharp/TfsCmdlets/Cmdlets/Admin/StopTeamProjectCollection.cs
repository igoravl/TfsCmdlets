using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// Stops a team project collection and make it offline.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    public class StopTeamProjectCollection : CmdletBase
    {
        /// <summary>
        /// Specifies the collection to stop.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Name")]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Server { get; set; }

        // TODO
    }
}
