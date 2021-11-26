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

    public static class ShellHelper
    {
        public static string GetPrompt()
        {
            const string ANSI_BG_BLUE = "\x1b[44m";
            const string ANSI_BG_CYAN = "\x1b[46m";
            const string ANSI_BG_MAGENTA = "\x1b[45m";
            const string ANSI_BG_GRAY = "\x1b[40;1m";
            const string ANSI_FG_GRAY = "\x1b[30;1m";
            const string ANSI_FG_WHITE = "\x1b[37;1m";
            const string ANSI_RESET = "\x1b[40m\x1b[0m";
            var NOT_CONNECTED = $"{Environment.NewLine}{ANSI_BG_GRAY}{ANSI_FG_GRAY}[Not connected]{ANSI_RESET}{Environment.NewLine}";

            var connections = ServiceLocator.Instance.GetExport<ICurrentConnections>();

            List<string> segments;
            string colorScheme;

            if (connections.Collection is { } tpc)
            {
                colorScheme = tpc.IsHosted ? $"{ANSI_BG_CYAN}{ANSI_FG_WHITE}" : $"{ANSI_BG_MAGENTA}{ANSI_FG_WHITE}";

                segments = new List<string>();

                switch (tpc.Uri.Host.ToLowerInvariant())
                {
                    case "dev.azure.com":
                    {
                        segments.Add("dev.azure.com");
                        segments.Add(tpc.DisplayName);
                        break;
                    }
                    case { } s when s.EndsWith(".visualstudio.com"):
                    {
                        segments.Add(s);
                        break;
                    }
                    default:
                    {
                        segments.Add(tpc.Uri.Host);
                        break;
                    }
                }
            }
            else
            {
                return NOT_CONNECTED;
            }

            if (connections.Project is { } tp) segments.Add(tp.Name);

            if (connections.Team is { } t) segments.Add(t.Name);
            
            var userName = tpc.AuthorizedIdentity.UniqueName ??
                           tpc.AuthorizedIdentity.Properties["Account"].ToString();

            return $"{Environment.NewLine}{colorScheme}[{string.Join(" > ", segments.ToArray())} {ANSI_BG_BLUE}({userName}){colorScheme}]{ANSI_RESET}{Environment.NewLine}";
        }
    }
}