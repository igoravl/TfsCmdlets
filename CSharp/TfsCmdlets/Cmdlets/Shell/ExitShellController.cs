namespace TfsCmdlets.Controllers.Shell
{
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