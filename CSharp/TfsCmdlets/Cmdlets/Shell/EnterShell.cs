using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Activates the Azure DevOps Shell.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Enter-TfsShell sets up an interactive Azure DevOps Shell session. It customizes the
    ///     PowerShell prompt, optionally integrates with Oh-My-Posh for an enhanced prompt
    ///     experience, clears the host screen, displays a version banner, and loads an optional
    ///     user profile script.
    ///   </para>
    ///   <para>
    ///     If Oh-My-Posh is installed and not disabled, it is automatically initialized using
    ///     the bundled Azure DevOps theme (<c>azuredevops.omp.json</c>). A custom theme path can
    ///     be provided via the <c>TFSCMDLETS_OMP_THEME</c> environment variable. To fall back to
    ///     the classic TfsCmdlets prompt regardless of Oh-My-Posh availability, set the
    ///     <c>TFSCMDLETS_OMP_DISABLE</c> environment variable to <c>1</c> or <c>true</c>.
    ///   </para>
    ///   <para>
    ///     When not running in debug mode, the user profile script is loaded from
    ///     <c>TfsCmdlets_Profile.ps1</c> (in the same directory as the PowerShell profile).
    ///     In debug mode, the script name is <c>TfsCmdlets_Debug_Profile.ps1</c>.
    ///   </para>
    ///   <para>Environment variables:</para>
    ///   <para>
    ///     * <c>TFSCMDLETS_OMP_THEME</c> — Full path to a custom Oh-My-Posh theme file
    ///       (<c>.omp.json</c>). When set, this theme is used instead of the bundled
    ///       <c>azuredevops.omp.json</c> theme.
    ///   </para>
    ///   <para>
    ///     * <c>TFSCMDLETS_OMP_DISABLE</c> — Set to <c>1</c> or <c>true</c> (case-insensitive)
    ///       to disable Oh-My-Posh integration entirely. When disabled, the classic TfsCmdlets
    ///       prompt is used regardless of whether Oh-My-Posh is installed.
    ///   </para>
    ///   <para>
    ///     To end the shell session and restore the previous prompt and window title, call
    ///     Exit-TfsShell.
    ///   </para>
    /// </remarks>
    [TfsCmdlet(CmdletScope.None)]
    partial class EnterShell
    {
        /// <summary>
        /// Specifies the shell window title. Defaults to "Azure DevOps Shell".
        /// </summary>
        [Parameter]
        public string WindowTitle { get; set; } = "Azure DevOps Shell";

        /// <summary>
        /// Does not clear the host screen when activating the Azure DevOps Shell. When set, the
        /// prompt is updated without clearing the screen.
        /// </summary>
        [Parameter]
        public SwitchParameter DoNotClearHost { get; set; }

        /// <summary>
        /// Does not display the version banner (module description, version, and client library
        /// version) when activating the Azure DevOps Shell.
        /// </summary>
        [Parameter]
        public SwitchParameter NoLogo { get; set; }

        /// <summary>
        /// Does not load the user profile script (<c>TfsCmdlets_Profile.ps1</c>, located in the
        /// same directory as the PowerShell profile) when activating the Azure DevOps Shell.
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
