using System;
using System.Collections;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Shell
{
    [Cmdlet(VerbsCommon.Enter, "TfsShell")]
    public class EnterShell : BaseCmdlet
    {
        [Parameter()]
        public string WindowTitle { get; set; } = "Azure DevOps Shell";

        [Parameter()]
        public SwitchParameter DoNotClearHost { get; set; }

        internal static bool IsInShell {get;set;}
        internal static string PrevShellTitle {get;set;}
        internal static ScriptBlock PrevPrompt {get;set;}

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
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

            var promptCode = File.ReadAllText(Path.Combine(
                MyInvocation.MyCommand.Module.ModuleBase,
                "Private/Prompt.ps1"
            ));

            this.InvokeCommand.InvokeScript(promptCode, false, PipelineResultTypes.None, null);

            if (!DoNotClearHost.IsPresent)
            {
                this.InvokeScript("Clear-Host");
            }

            var manifest = MyInvocation.MyCommand.Module;
            var privateData = (Hashtable)manifest.PrivateData;

            WriteObject($"TfsCmdlets: {manifest.Description}");
            WriteObject($"Version {privateData["Build"]}");
            WriteObject($"Azure DevOps Client Library version {privateData["TfsClientVersion"]}");
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
