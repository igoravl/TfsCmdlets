using System.Collections;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Activates the Azure DevOps Shell
    /// </summary>
    [Cmdlet(VerbsCommon.Enter, "TfsShell")]
    public class EnterShell : CmdletBase
    {
        /// <summary>
        /// Specifies the shell window title. If omitted, defaults to "Azure DevOps Shell".
        /// </summary>
        [Parameter()]
        public string WindowTitle { get; set; } = "Azure DevOps Shell";

        /// <summary>
        /// Do not clear the host screen when activating the Azure DevOps Shell. When set, the
        /// prompt is enabled without clearing the screen.
        /// </summary>
        [Parameter()]
        public SwitchParameter DoNotClearHost { get; set; }

        /// <summary>
        /// Do not show the version banner when activating the Azure DevOps Shell.
        /// </summary>
        [Parameter()]
        public SwitchParameter NoLogo { get; set; }
    }
}
