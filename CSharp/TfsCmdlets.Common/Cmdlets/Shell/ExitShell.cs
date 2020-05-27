using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Shell
{
    [Cmdlet(VerbsCommon.Exit, "Shell")]
    public class ExitShell : BaseCmdlet
    {
        protected override void EndProcessing()
        {
            if(!EnterShell.IsInShell) return;

            if (!string.IsNullOrEmpty(EnterShell.PrevShellTitle))
            {
                Host.UI.RawUI.WindowTitle = EnterShell.PrevShellTitle;
            }

            if (EnterShell.PrevPrompt != null)
            {
                this.InvokeScript("Set-Content function:prompt $args[0]", EnterShell.PrevPrompt);
            }

            EnterShell.IsInShell = false;

            this.InvokeScript("Clear-Host");
        }
    }
}