using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Activates the Azure DevOps Shell
    /// </summary>
    [TfsCmdlet(CmdletScope.None)]
    partial class EnterShell
    {
        /// <summary>
        /// Specifies the shell window title. If omitted, defaults to "Azure DevOps Shell".
        /// </summary>
        [Parameter]
        public string WindowTitle { get; set; } = "Azure DevOps Shell";

        /// <summary>
        /// Do not clear the host screen when activating the Azure DevOps Shell. When set, the
        /// prompt is enabled without clearing the screen.
        /// </summary>
        [Parameter]
        public SwitchParameter DoNotClearHost { get; set; }

        /// <summary>
        /// Do not show the version banner when activating the Azure DevOps Shell.
        /// </summary>
        [Parameter]
        public SwitchParameter NoLogo { get; set; }
    }
}
