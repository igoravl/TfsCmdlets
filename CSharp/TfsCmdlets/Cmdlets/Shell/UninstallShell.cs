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
        private IUninstallShellFileSystem _fileSystem = new UninstallShellFileSystem();

        internal UninstallShellController(
            IPowerShellService powerShell,
            IDataManager data,
            IParameterManager parameters,
            ILogger logger,
            IUninstallShellFileSystem fileSystem)
            : base(powerShell, data, parameters, logger)
        {
            _fileSystem = fileSystem;
        }

        private static readonly string[] ShortcutNames = {
            "Azure DevOps Shell.lnk",
            "Azure DevOps Shell (Core).lnk"
        };

        protected override IEnumerable Run()
        {
            if (!_fileSystem.IsWindows)
            {
                Logger.LogWarn("Uninstall-TfsShell is only supported on Windows.");
                yield break;
            }

            if (Target.HasFlag(ShellTarget.StartMenu))
            {
                var startMenuDir = Path.Combine(
                    _fileSystem.GetFolderPath(Environment.SpecialFolder.StartMenu),
                    "Programs", "TfsCmdlets");

                RemoveShortcut(startMenuDir, "Start Menu");
            }

            if (Target.HasFlag(ShellTarget.Desktop))
            {
                var desktopPath = _fileSystem.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
            foreach (var shortcutFile in ShortcutNames)
            {
                var shortcutPath = Path.Combine(folder, shortcutFile);

                if (!_fileSystem.FileExists(shortcutPath))
                {
                    Logger.Log($"No {locationName} shortcut found at {shortcutPath}. Skipping.");
                    continue;
                }

                if (!PowerShell.ShouldProcess(shortcutPath, $"Remove {locationName} shortcut"))
                    continue;

                try
                {
                    _fileSystem.DeleteFile(shortcutPath);
                    Logger.Log($"Removed {locationName} shortcut: {shortcutPath}");
                }
                catch (Exception ex)
                {
                    Logger.LogWarn($"Failed to remove {locationName} shortcut: {ex.Message}");
                }
            }

            // Clean up the folder if now empty (e.g. Start Menu subfolder)
            try
            {
                if (_fileSystem.DirectoryExists(folder) && _fileSystem.GetFileSystemEntries(folder).Length == 0)
                    _fileSystem.DeleteDirectory(folder);
            }
            catch { /* ignore cleanup failures */ }
        }

        private void RemoveTerminalFragments()
        {
            var localAppData = _fileSystem.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var candidatePaths = new[]
            {
                Path.Combine(localAppData, "Microsoft", "Windows Terminal", "Fragments", "TfsCmdlets"),
                Path.Combine(localAppData, "Microsoft", "Windows Terminal Preview", "Fragments", "TfsCmdlets")
            };

            foreach (var fragmentDir in candidatePaths)
            {
                if (!_fileSystem.DirectoryExists(fragmentDir))
                    continue;

                foreach (var file in _fileSystem.GetFiles(fragmentDir, "AzureDevOpsShell-*.json"))
                {
                    if (!PowerShell.ShouldProcess(file, "Remove Windows Terminal profile fragment"))
                        continue;

                    try
                    {
                        _fileSystem.DeleteFile(file);
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
                    if (_fileSystem.DirectoryExists(fragmentDir) && _fileSystem.GetFileSystemEntries(fragmentDir).Length == 0)
                    {
                        _fileSystem.DeleteDirectory(fragmentDir);
                    }
                }
                catch { /* ignore */ }
            }
        }
    }

    internal interface IUninstallShellFileSystem
    {
        bool IsWindows { get; }
        string GetFolderPath(Environment.SpecialFolder folder);
        bool FileExists(string path);
        void DeleteFile(string path);
        bool DirectoryExists(string path);
        string[] GetFileSystemEntries(string path);
        void DeleteDirectory(string path);
        string[] GetFiles(string path, string searchPattern);
    }

    internal sealed class UninstallShellFileSystem : IUninstallShellFileSystem
    {
        public bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public string GetFolderPath(Environment.SpecialFolder folder) => Environment.GetFolderPath(folder);

        public bool FileExists(string path) => File.Exists(path);

        public void DeleteFile(string path) => File.Delete(path);

        public bool DirectoryExists(string path) => Directory.Exists(path);

        public string[] GetFileSystemEntries(string path) => Directory.GetFileSystemEntries(path);

        public void DeleteDirectory(string path) => Directory.Delete(path);

        public string[] GetFiles(string path, string searchPattern) => Directory.GetFiles(path, searchPattern);
    }
}
