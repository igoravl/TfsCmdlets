using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Shell
{
    [Cmdlet(VerbsCommon.Enter, "Shell")]
    public class EnterShell
    {
        /*
            [CmdletBinding()]
            Param
            (
                # Specifies the shell window title. If omitted, defaults to "Azure DevOps Shell"
                [Parameter()]
                public string WindowTitle { get; set; } = "Azure DevOps Shell",

                # Do not clear screen on entering shell
                [Parameter()]
                public SwitchParameter DoNotClearHost { get; set; }

            if(script:IsInShell)
            {
                # Avoid reentrance
                return
            }

            # Persist current values for later restoring

            if(! script:PrevShellTitle)
            {
                script:PrevShellTitle = Host.UI.RawUI.WindowTitle

                if(Test-Path function:prompt)
                {
                    script:PrevPrompt = Get-Content function:prompt
                }
            }

            # Replace title and prompt

            Host.UI.RawUI.WindowTitle = WindowTitle
            Set-Content function:prompt {_TfsCmdletsPrompt}

            # Show banner

            if(! DoNotClearHost.IsPresent)
            {
                Clear-Host
            }

            module = Test-ModuleManifest -Path (Join-Path MyInvocation.MyCommand.Module.ModuleBase "TfsCmdlets.psd1")
            Write-Output $"TfsCmdlets: {{module}.Description}"
            Write-Output $"Version {{module}.PrivateData.Build}"
            Write-Output $"Azure DevOps Client Library version {{module}.PrivateData.TfsClientVersion}"
            Write-Output ""
            Write-Output $"Loading TfsCmdlets module took {{global}:TfsCmdletsLoadSw.ElapsedMilliseconds}ms."

            profileScript = Join-Path $(System.Environment.GetFolderPath("MyDocuments")) "WindowsPowerShell/TfsCmdlets_Profile.ps1"

            if(Test-Path (profileScript))
            {
                sw = System.Diagnostics.Stopwatch.StartNew()
                . profileScript
                sw.Stop()

                Write-Output $"Loading TfsCmdlets profile took {{sw}.ElapsedMilliseconds}ms."
            }

            script:IsInShell = true

            Write-Output ""
        }
        */
    }
}
