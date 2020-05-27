using System;
using System.Collections;
using System.IO;
using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Shell
{
    [Cmdlet(VerbsCommon.Enter, "Shell")]
    public class EnterShell : BaseCmdlet
    {
        [Parameter()]
        public string WindowTitle { get; set; } = "Azure DevOps Shell";

        [Parameter()]
        public SwitchParameter DoNotClearHost { get; set; }

        internal static bool IsInShell {get;set;}
        internal static string PrevShellTitle {get;set;}
        internal static ScriptBlock PrevPrompt {get;set;}

        protected override void EndProcessing()
        {
            if (IsInShell)
            {
                return;
            }

            PrevShellTitle ??= Host.UI.RawUI.WindowTitle;

            if (this.InvokeScript<bool>("Test-Path function:prompt"))
            {
                PrevPrompt = this.InvokeScript<ScriptBlock>("Get-Content function:prompt");
            }

            Host.UI.RawUI.WindowTitle = WindowTitle;

            var prompt = ScriptBlock.Create(@"
$promptPrefix = '[Not connected]'
$defaultPsPrompt = ""$($ExecutionContext.SessionState.Path.CurrentLocation)$('>' * ($NestedPromptLevel + 1)) ""
$backColor = 'DarkGray'
$foreColor = 'White'

$server = (Get-TfsConfigurationServer -Current)

if($server)
{
    $tpc = (Get-TfsTeamProjectCollection -Current); $tp = (Get-TfsTeamProject -Current); $t = (Get-TfsTeam -Current)
    $serverName = $server.Name; $userName = $server.AuthorizedIdentity.UniqueName

    if ($serverName -like '*.visualstudio.com')
    {
        $tpcName = $serverName.SubString(0, $serverName.IndexOf('.'))
        $promptPrefix = ""[AzDev:/$tpcName""
        $backColor = 'DarkBlue'
        $foreColor = 'White'
    }
    elseif ($serverName -eq 'dev.azure.com')
    {
        $tpcName = $server.Uri.Segments[1]
        $promptPrefix = ""[AzDev:/$tpcName""
        $backColor = 'DarkBlue'
        $foreColor = 'White'
    }
    else
    {
        $promptPrefix = ""[TFS:/$($server.Uri.Host)/""
        $backColor = 'DarkMagenta'
        $foreColor = 'White'

        if ($tpc)
        {
            $promptPrefix += ""$($tpc.Name)""
        }
    }

    if ($tp)
    {
        $promptPrefix += ""/$($tp.Name)""
    }

    if ($t)
    {
        $promptPrefix += ""/$($t.Name)""
    }

    if($userName)
    {
        $promptPrefix += "" ($userName)""
    }

    $promptPrefix += ']'

}

Write-Host -Object $promptPrefix -ForegroundColor $foreColor -BackgroundColor $backColor # -NoNewline

return $defaultPsPrompt
            ");

            this.InvokeScript("Set-Content function:prompt $args[0]", prompt);

            if (!DoNotClearHost.IsPresent)
            {
                this.InvokeScript("Clear-Host");
            }

            var manifest = MyInvocation.MyCommand.Module;
            var privateData = (Hashtable)manifest.PrivateData;

            WriteObject($"TfsCmdlets: {manifest.Description}");
            WriteObject($"Version {privateData["Build"]}");
            WriteObject($"Azure DevOps Client Library version {privateData["TfsClientVersion"]}");
            WriteObject("");
            // WriteObject($"Loading TfsCmdlets module took {{global}:TfsCmdletsLoadSw.ElapsedMilliseconds}ms."

            var profileDir = Path.GetDirectoryName((string)((PSObject) this.GetVariableValue("PROFILE")).BaseObject);
            var profilePath = Path.Combine(profileDir, "TfsCmdlets_Profile.ps1");

            if (File.Exists(profilePath))
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();

                try
                {
                    this.InvokeScript($". '{profilePath}'");
                }
                catch(CmdletInvocationException ex)
                {
                    WriteWarning($"Error loading profile {profilePath}: {ex.Message}");
                    WriteWarning($"{ex.ErrorRecord.InvocationInfo.PositionMessage}");
                    WriteObject("");
                }

                sw.Stop();
                WriteObject($"Loading TfsCmdlets profile took {sw.ElapsedMilliseconds}ms.");
            }

            IsInShell = true;

            WriteObject("");
        }
    }
}
