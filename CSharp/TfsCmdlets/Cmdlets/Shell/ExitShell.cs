using System.Collections.Generic;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Deactivates the Azure DevOps Shell
    /// </summary>
    [Cmdlet(VerbsCommon.Exit, "TfsShell")]
    public class ExitShell : CmdletBase
    {
        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    if(!EnterShell.IsInShell) return;

        //    if (!string.IsNullOrEmpty(EnterShell.PrevShellTitle))
        //    {
        //        Host.UI.RawUI.WindowTitle = EnterShell.PrevShellTitle;
        //    }

        //    if (EnterShell.PrevPrompt != null)
        //    {
        //        this.InvokeScript("Set-Content function:prompt $OldPrompt", new Dictionary<string,object>() {
        //            ["OldPrompt"] = EnterShell.PrevPrompt
        //        });
        //    }

        //    EnterShell.IsInShell = false;

        //    this.InvokeScript("Clear-Host");
        //}
    }
}