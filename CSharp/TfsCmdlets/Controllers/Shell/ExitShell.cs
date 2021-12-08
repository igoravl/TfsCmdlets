using System;
using System.Collections;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.Shell
{
    [CmdletController]
    partial class ExitShell
    {
        public override object InvokeCommand()
        {
           if(!EnterShell.IsInShell) return null;

           if (!string.IsNullOrEmpty(EnterShell.PrevShellTitle))
           {
               PowerShell.WindowTitle = EnterShell.PrevShellTitle;
           }

           if (EnterShell.PrevPrompt != null)
           {
               PowerShell.InvokeScript("Set-Content function:prompt $OldPrompt", new Dictionary<string,object>() {
                   ["OldPrompt"] = EnterShell.PrevPrompt
               });
           }

           EnterShell.IsInShell = false;

           PowerShell.InvokeScript("Clear-Host");

           return null;
        }
    }
}