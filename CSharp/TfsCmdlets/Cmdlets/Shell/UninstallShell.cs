using System.Management.Automation;
using System.Runtime.InteropServices;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Removes Azure DevOps Shell shortcuts and Windows Terminal profile fragments.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Uninstall-TfsShell removes shortcuts and profile fragments previously created by
    ///     Install-TfsShell. By default, it removes items from all targets (Start Menu,
    ///     Desktop, and Windows Terminal). Use the <c>-Target</c> parameter for selective removal.
    ///   </para>
    /// </remarks>
    [TfsCmdlet(CmdletScope.None, SupportsShouldProcess = true)]
    partial class UninstallShell
    {
        /// <summary>
        /// Specifies the target locations to remove shortcuts and profile fragments from.
        /// Defaults to All (StartMenu, Desktop, and WindowsTerminal).
        /// </summary>
        [Parameter(Position = 0)]
        public ShellTarget Target { get; set; } = ShellTarget.All;
    }

    [CmdletController]
    partial class UninstallShellController
    {
        private const string ShortcutName = "Azure DevOps Shell.lnk";

        protected override IEnumerable Run()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Logger.LogWarn("Uninstall-TfsShell is only supported on Windows.");
                yield break;
            }

            if (Target.HasFlag(ShellTarget.StartMenu))
            {
                var startMenuDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
                    "Programs", "TfsCmdlets");

                RemoveShortcut(startMenuDir, "Start Menu");
            }

            if (Target.HasFlag(ShellTarget.Desktop))
            {
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                RemoveShortcut(desktopPath, "Desktop");
            }

            if (Target.HasFlag(ShellTarget.WindowsTerminal))
            {
                RemoveTerminalFragments();
            }

            yield break;
        }

        private void RemoveShortcut(string folder, string locationName)
        {
            var shortcutPath = Path.Combine(folder, ShortcutName);

            if (!File.Exists(shortcutPath))
            {
                Logger.Log($"No {locationName} shortcut found at {shortcutPath}. Skipping.");
                return;
            }

            if (!PowerShell.ShouldProcess(shortcutPath, $"Remove {locationName} shortcut"))
                return;

            try
            {
                File.Delete(shortcutPath);
                Logger.Log($"Removed {locationName} shortcut: {shortcutPath}");

                // Clean up the folder if empty (Start Menu subfolder)
                if (Directory.Exists(folder) && Directory.GetFileSystemEntries(folder).Length == 0)
                {
                    Directory.Delete(folder);
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarn($"Failed to remove {locationName} shortcut: {ex.Message}");
            }
        }

        private void RemoveTerminalFragments()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var candidatePaths = new[]
            {
                Path.Combine(localAppData, "Microsoft", "Windows Terminal", "Fragments", "TfsCmdlets"),
                Path.Combine(localAppData, "Microsoft", "Windows Terminal Preview", "Fragments", "TfsCmdlets")
            };

            foreach (var fragmentDir in candidatePaths)
            {
                if (!Directory.Exists(fragmentDir))
                    continue;

                foreach (var file in Directory.GetFiles(fragmentDir, "AzureDevOpsShell-*.json"))
                {
                    if (!PowerShell.ShouldProcess(file, "Remove Windows Terminal profile fragment"))
                        continue;

                    try
                    {
                        File.Delete(file);
                        Logger.Log($"Removed Windows Terminal fragment: {file}");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogWarn($"Failed to remove fragment {file}: {ex.Message}");
                    }
                }

                // Clean up directory if empty
                try
                {
                    if (Directory.Exists(fragmentDir) && Directory.GetFileSystemEntries(fragmentDir).Length == 0)
                    {
                        Directory.Delete(fragmentDir);
                    }
                }
                catch { /* ignore */ }
            }
        }
    }
}
