using System;
using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    ///   Triggers an Identity Sync server job.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "TfsIdentitySync", SupportsShouldProcess = true)]
    [TfsCmdlet(CmdletScope.Collection, DesktopOnly = true)]
    partial class StartIdentitySync
    {
        /// <summary>
        /// Waits until the job finishes running. If omitted, the identity sync job will run asynchronously.
        /// </summary>
        [Parameter]
        public SwitchParameter Wait { get; set; }
    }
}