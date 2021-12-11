using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    ///   Triggers an Identity Sync server job.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, DesktopOnly = true, SupportsShouldProcess = true)]
    partial class StartIdentitySync
    {
        /// <summary>
        /// Waits until the job finishes running. If omitted, the identity sync job will run asynchronously.
        /// </summary>
        [Parameter]
        public SwitchParameter Wait { get; set; }
    }
}