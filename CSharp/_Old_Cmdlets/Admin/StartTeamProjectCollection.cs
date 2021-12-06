using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// Starts an offline team project collection and make it online.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    public class StartTeamProjectCollection : Cmdlet
    {
        /// <summary>
        /// Specifies the name of the collection to start.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Server { get; set; }

        override protected void ProcessRecord()
        {
            WriteObject($"Collection: {Collection}");
        }
    }
}