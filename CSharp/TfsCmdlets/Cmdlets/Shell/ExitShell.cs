namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Deactivates the Azure DevOps Shell
    /// </summary>
    [TfsCmdlet(CmdletScope.None)]
    partial class ExitShell 
    {
    }

    [CmdletController]
    partial class ExitShellController
    {
        protected override IEnumerable Run()
        {
           if(!EnterShellController.IsInShell) return null;

           if (!string.IsNullOrEmpty(EnterShellController.PrevShellTitle))
           {
               PowerShell.WindowTitle = EnterShellController.PrevShellTitle;
           }

           if (EnterShellController.PrevPrompt != null)
           {
               PowerShell.InvokeScript("Set-Content function:prompt $OldPrompt", new Dictionary<string,object>() {
                   ["OldPrompt"] = EnterShellController.PrevPrompt
               });
           }

           EnterShellController.IsInShell = false;

           PowerShell.InvokeScript("Clear-Host");

           return null;
        }
    }
}