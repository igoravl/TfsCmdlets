using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    ///   Triggers an Identity Sync server job.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "TfsIdentitySync", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [DesktopOnly]
    public partial class StartIdentitySync : BaseCmdlet
    {
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Server { get; set; }

        /// <summary>
        /// Waits until the job finishes running. If omitted, the identity sync job will run asynchronously.
        /// </summary>
        [Parameter()]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// HELP_PARAM_CREDENTIAL
        /// </summary>
        [Parameter()]
        public object Credential { get; set; }

        partial void DoProcessRecord();

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            DoProcessRecord();
        }
    }
}