using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Installs Azure DevOps Shell shortcuts and Windows Terminal profile fragments.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Install-TfsShell creates shell shortcuts that launch a PowerShell session with the
    ///     TfsCmdlets module pre-loaded and Enter-TfsShell invoked. When Windows Terminal is
    ///     detected, it creates shortcuts that launch via <c>wt.exe</c> and deploys JSON
    ///     profile fragments. Use <c>-Force</c> to create traditional PowerShell shortcuts
    ///     even when Windows Terminal is available.
    ///   </para>
    ///   <para>
    ///     Windows Terminal detection uses registry lookups and file system checks. If detection
    ///     fails (e.g. due to insufficient permissions), the cmdlet falls back to PowerShell
    ///     shortcuts automatically.
    ///   </para>
    /// </remarks>
    [TfsCmdlet(CmdletScope.None, SupportsShouldProcess = true)]
    partial class InstallShell
    {
        /// <summary>
        /// Specifies the target locations for shortcut installation. Defaults to StartMenu and Desktop.
        /// </summary>
        [Parameter(Position = 0)]
        public ShellTarget Target { get; set; } = ShellTarget.StartMenu | ShellTarget.Desktop;

        /// <summary>
        /// Forces the creation of traditional PowerShell shortcuts even when Windows Terminal
        /// is detected. Does not affect the WindowsTerminal target.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController]
    partial class InstallShellController
    {
        private const string ShortcutName = "Azure DevOps Shell.lnk";
        private const string ShellCommand = "-noexit -command \"Import-Module TfsCmdlets; Enter-TfsShell\"";

        protected override IEnumerable Run()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Logger.LogWarn("Install-TfsShell is only supported on Windows.");
                yield break;
            }

            var modulePath = PowerShell.Module.ModuleBase;
            var iconPath = Path.Combine(modulePath, "TfsCmdletsShell.ico");
            var hasWindowsTerminal = !Force && DetectWindowsTerminal();

            if (Target.HasFlag(ShellTarget.StartMenu))
            {
                var startMenuPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
                    "Programs", "TfsCmdlets");

                InstallShortcut(startMenuPath, hasWindowsTerminal, iconPath, "Start Menu");
            }

            if (Target.HasFlag(ShellTarget.Desktop))
            {
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                InstallShortcut(desktopPath, hasWindowsTerminal, iconPath, "Desktop");
            }

            if (Target.HasFlag(ShellTarget.WindowsTerminal))
            {
                InstallTerminalFragments(modulePath);
            }

            yield break;
        }

        private void InstallShortcut(string folder, bool useWindowsTerminal, string iconPath, string locationName)
        {
            var shortcutPath = Path.Combine(folder, ShortcutName);

            if (!PowerShell.ShouldProcess(shortcutPath, $"Create {locationName} shortcut"))
                return;

            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string targetExe, arguments;

                if (useWindowsTerminal)
                {
                    targetExe = FindWtExePath() ?? "wt.exe";
                    arguments = $"new-tab --title \"Azure DevOps Shell\" -- powershell.exe {ShellCommand}";
                }
                else
                {
                    targetExe = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.System),
                        @"WindowsPowerShell\v1.0\powershell.exe");
                    arguments = ShellCommand;
                }

                CreateShortcutFile(shortcutPath, targetExe, arguments, iconPath);
                Logger.Log($"Created {locationName} shortcut: {shortcutPath}");
            }
            catch (Exception ex)
            {
                Logger.LogWarn($"Failed to create {locationName} shortcut: {ex.Message}");
            }
        }

        private void InstallTerminalFragments(string modulePath)
        {
            var fragmentsBase = GetTerminalFragmentsPath();

            if (fragmentsBase == null)
            {
                Logger.LogWarn("Windows Terminal not detected. Skipping profile fragment installation.");
                return;
            }

            var fragmentDir = Path.Combine(fragmentsBase, "TfsCmdlets");
            var sourceDir = Path.Combine(modulePath, "_Fragments");

            foreach (var fragmentName in new[] { "AzureDevOpsShell-WinPS.json", "AzureDevOpsShell-PSCore.json" })
            {
                var sourcePath = Path.Combine(sourceDir, fragmentName);
                var destPath = Path.Combine(fragmentDir, fragmentName);

                if (!File.Exists(sourcePath))
                {
                    Logger.LogWarn($"Fragment source not found: {sourcePath}");
                    continue;
                }

                if (!PowerShell.ShouldProcess(destPath, "Deploy Windows Terminal profile fragment"))
                    continue;

                try
                {
                    if (!Directory.Exists(fragmentDir))
                        Directory.CreateDirectory(fragmentDir);

                    File.Copy(sourcePath, destPath, overwrite: true);
                    Logger.Log($"Deployed Windows Terminal fragment: {destPath}");
                }
                catch (Exception ex)
                {
                    Logger.LogWarn($"Failed to deploy fragment {fragmentName}: {ex.Message}");
                }
            }
        }

        private static bool DetectWindowsTerminal()
        {
            try
            {
                // Check App Paths registry
                using var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\App Paths\wt.exe");
                if (key != null) return true;
            }
            catch
            {
                // Registry access failure — fall through
            }

            // Check well-known install locations
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            // Microsoft Store package
            var packagesPath = Path.Combine(localAppData, "Microsoft", "WindowsApps", "wt.exe");
            if (File.Exists(packagesPath)) return true;

            // Check PATH
            var pathDirs = Environment.GetEnvironmentVariable("PATH")?.Split(';') ?? Array.Empty<string>();
            foreach (var dir in pathDirs)
            {
                try
                {
                    if (File.Exists(Path.Combine(dir.Trim(), "wt.exe"))) return true;
                }
                catch
                {
                    // Skip invalid path entries
                }
            }

            return false;
        }

        private static string FindWtExePath()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\App Paths\wt.exe");
                var val = key?.GetValue(null) as string;
                if (!string.IsNullOrEmpty(val) && File.Exists(val)) return val;
            }
            catch { /* ignore */ }

            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var storePath = Path.Combine(localAppData, "Microsoft", "WindowsApps", "wt.exe");
            if (File.Exists(storePath)) return storePath;

            return null;
        }

        private static string GetTerminalFragmentsPath()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            // Standard Windows Terminal
            var fragmentsPath = Path.Combine(localAppData, "Microsoft", "Windows Terminal", "Fragments");
            if (Directory.Exists(Path.Combine(localAppData, "Microsoft", "Windows Terminal")))
                return fragmentsPath;

            // Windows Terminal Preview
            var previewPath = Path.Combine(localAppData, "Microsoft", "Windows Terminal Preview", "Fragments");
            if (Directory.Exists(Path.Combine(localAppData, "Microsoft", "Windows Terminal Preview")))
                return previewPath;

            // Detect via packages directory (Store install)
            var packagesDir = Path.Combine(localAppData, "Packages");
            if (Directory.Exists(packagesDir))
            {
                try
                {
                    foreach (var dir in Directory.GetDirectories(packagesDir, "Microsoft.WindowsTerminal*"))
                    {
                        return fragmentsPath; // Use standard path for Store installs
                    }
                }
                catch { /* ignore */ }
            }

            return null;
        }

        private void CreateShortcutFile(string shortcutPath, string targetExe, string arguments, string iconPath)
        {
            var script = @"
$shell = New-Object -ComObject WScript.Shell
$sc = $shell.CreateShortcut($ShortcutPath)
$sc.TargetPath = $TargetExe
$sc.Arguments = $ShortcutArgs
$sc.WorkingDirectory = $WorkDir
if ($IconPath -and (Test-Path $IconPath)) { $sc.IconLocation = $IconPath }
$sc.Save()
[System.Runtime.InteropServices.Marshal]::ReleaseComObject($shell) | Out-Null
";
            PowerShell.InvokeScript(script, new Dictionary<string, object>
            {
                ["ShortcutPath"] = shortcutPath,
                ["TargetExe"] = targetExe,
                ["ShortcutArgs"] = arguments,
                ["IconPath"] = iconPath,
                ["WorkDir"] = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            });
        }
    }
}
