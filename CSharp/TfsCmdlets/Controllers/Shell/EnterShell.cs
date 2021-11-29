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
    internal class EnterShell : ControllerBase
    {
        private IPowerShellService PowerShell { get; }

        public override object InvokeCommand(ParameterDictionary parameters)
        {
            if (IsInShell) return null;

            var doNotClearHost = parameters.Get<bool>("DoNotClearHost");
            var noLogo = parameters.Get<bool>("NoLogo");
            var windowTitle = parameters.Get<string>("WindowTitle");

            PrevShellTitle ??= PowerShell.WindowTitle;

            if (PowerShell.InvokeScript<bool>("Test-Path function:prompt"))
            {
                PrevPrompt = PowerShell.InvokeScript<ScriptBlock>("Get-Content function:prompt");
            }

            PowerShell.WindowTitle = windowTitle;

            const string promptCode = @"
Function prompt { return ([TfsCmdlets.ShellHelper]::GetPrompt()) + `
    ""$($ExecutionContext.SessionState.Path.CurrentLocation)$('>' * ($NestedPromptLevel + 1)) ""}";

            PowerShell.InvokeScript(promptCode, false, PipelineResultTypes.None, null);

            if (!doNotClearHost)
            {
                PowerShell.InvokeScript("Clear-Host");
            }

            var manifest = PowerShell.Module;
            var privateData = (Hashtable)manifest.PrivateData;

            if (!noLogo)
            {
                PowerShell.WriteObject($"TfsCmdlets: {manifest.Description}");
                PowerShell.WriteObject($"Version {privateData?["Build"] ?? "N/A"}");
                PowerShell.WriteObject($"Azure DevOps Client Library version {privateData?["TfsClientVersion"] ?? "N/A"}");
            }

            // WriteObject($"Loading TfsCmdlets module took {{global}:TfsCmdletsLoadSw.ElapsedMilliseconds}ms."

            var profileDir = Path.GetDirectoryName((string)((PSObject)PowerShell.GetVariableValue("PROFILE")).BaseObject);
            var profilePath = Path.Combine(profileDir, $"TfsCmdlets_{(System.Diagnostics.Debugger.IsAttached ? "Debug_" : "")}Profile.ps1");

            if (File.Exists(profilePath))
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();

                try
                {
                    PowerShell.InvokeScript($". '{profilePath}'");
                }
                catch (CmdletInvocationException ex)
                {
                    PowerShell.WriteWarning($"Error loading profile {profilePath}: {ex.Message}");
                    PowerShell.WriteWarning($"{ex.ErrorRecord.InvocationInfo.PositionMessage}");
                    PowerShell.WriteObject("");
                }

                sw.Stop();

                if (!noLogo)
                {
                    PowerShell.WriteObject($"Loading TfsCmdlets {(System.Diagnostics.Debugger.IsAttached ? "debug " : "")}profile took {sw.ElapsedMilliseconds}ms.");
                }
            }
            else
            {
                PowerShell.WriteVerbose($"No profile found at {profilePath}");
            }

            IsInShell = true;

            return string.Empty;
        }

        internal static bool IsInShell { get; set; }

        internal static string PrevShellTitle { get; set; }

        internal static ScriptBlock PrevPrompt { get; set; }

        [ImportingConstructor]
        public EnterShell(IPowerShellService powerShell, ILogger logger)
            : base(logger)
        {
            PowerShell = powerShell;
        }
    }

}