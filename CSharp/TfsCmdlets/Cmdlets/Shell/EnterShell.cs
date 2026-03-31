using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Activates the Azure DevOps Shell
    /// </summary>
    [TfsCmdlet(CmdletScope.None)]
    partial class EnterShell
    {
        /// <summary>
        /// Specifies the shell window title. If omitted, defaults to "Azure DevOps Shell".
        /// </summary>
        [Parameter]
        public string WindowTitle { get; set; } = "Azure DevOps Shell";

        /// <summary>
        /// Do not clear the host screen when activating the Azure DevOps Shell. When set, the
        /// prompt is enabled without clearing the screen.
        /// </summary>
        [Parameter]
        public SwitchParameter DoNotClearHost { get; set; }

        /// <summary>
        /// Do not show the version banner when activating the Azure DevOps Shell.
        /// </summary>
        [Parameter]
        public SwitchParameter NoLogo { get; set; }

        /// <summary>
        /// Do not load the user profile TfsCmdlets.Profile.ps1 (if present) when activating the Azure DevOps Shell.
        /// </summary>
        [Parameter]
        public SwitchParameter NoProfile { get; set; }

    }

    [CmdletController]
    partial class EnterShellController
    {
        protected override IEnumerable Run()
        {
            if (IsInShell) return null;

            var ohMyPoshTheme = Environment.GetEnvironmentVariable("TFSCMDLETS_OMP_THEME");
            var noOhMyPosh =
                string.Equals(Environment.GetEnvironmentVariable("TFSCMDLETS_OMP_DISABLE"), "1") ||
                string.Equals(Environment.GetEnvironmentVariable("TFSCMDLETS_OMP_DISABLE"), "true", StringComparison.OrdinalIgnoreCase);

            PrevShellTitle ??= PowerShell.WindowTitle;

            if (PowerShell.InvokeScript<bool>("Test-Path function:prompt"))
            {
                PrevPrompt = PowerShell.InvokeScript<ScriptBlock>("Get-Content function:prompt");
            }

            PowerShell.WindowTitle = WindowTitle;

            var useOhMyPosh = false;

            if (!noOhMyPosh)
            {
                useOhMyPosh = PowerShell.InvokeScript<bool>(
                    "$null -ne (Get-Command oh-my-posh -ErrorAction SilentlyContinue)");
            }

            if (useOhMyPosh)
            {
                var themePath = ohMyPoshTheme;

                if (string.IsNullOrEmpty(themePath))
                {
                    // Use the bundled Azure DevOps theme
                    var modulePath = PowerShell.Module.ModuleBase;
                    themePath = Path.Combine(modulePath, "azuredevops.omp.json");
                }

                if (!File.Exists(themePath))
                {
                    PowerShell.WriteWarning($"Oh-My-Posh theme not found at '{themePath}'. Falling back to classic prompt.");
                    useOhMyPosh = false;
                }
                else
                {
                    var escapedPath = themePath.Replace("'", "''");
                    try
                    {
                        PowerShell.InvokeScript(
                            $"oh-my-posh init pwsh --config '{escapedPath}' | Invoke-Expression",
                            false, PipelineResultTypes.None, null);

                        UsedOhMyPosh = true;
                    }
                    catch (RuntimeException ex)
                    {
                        PowerShell.WriteWarning($"Failed to initialize Oh-My-Posh: {ex.Message}. Falling back to classic prompt.");
                        useOhMyPosh = false;
                        UsedOhMyPosh = false;
                    }
                }
            }

            if (!useOhMyPosh)
            {
                const string promptCode = @"
Function prompt { return ([TfsCmdlets.ShellHelper]::GetPrompt()) + `
    ""$($ExecutionContext.SessionState.Path.CurrentLocation)$('>' * ($NestedPromptLevel + 1)) ""}";

                PowerShell.InvokeScript(promptCode, false, PipelineResultTypes.None, null);
            }

            if (!DoNotClearHost)
            {
                PowerShell.InvokeScript("Clear-Host");
            }

            var manifest = PowerShell.Module;
            var privateData = (Hashtable)manifest.PrivateData;

            if (!NoLogo)
            {
                PowerShell.WriteObject($"TfsCmdlets: {manifest.Description}");
                PowerShell.WriteObject($"Version {privateData?["Build"] ?? "N/A"}");
                PowerShell.WriteObject($"Azure DevOps Client Library version {privateData?["TfsClientVersion"] ?? "N/A"}");
            }

            // WriteObject($"Loading TfsCmdlets module took {{global}:TfsCmdletsLoadSw.ElapsedMilliseconds}ms."

            var profileDir = Path.GetDirectoryName((string)((PSObject)PowerShell.GetVariableValue("PROFILE")).BaseObject);
            var profilePath = Path.Combine(profileDir, $"TfsCmdlets_{(System.Diagnostics.Debugger.IsAttached ? "Debug_" : "")}Profile.ps1");

            if ((!NoProfile) && File.Exists(profilePath))
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

                if (!NoLogo)
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

        internal static bool UsedOhMyPosh { get; set; }

        internal static string PrevShellTitle { get; set; }

        internal static ScriptBlock PrevPrompt { get; set; }
    }
}
