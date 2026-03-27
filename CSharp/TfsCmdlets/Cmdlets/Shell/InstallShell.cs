using System.Runtime.InteropServices;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Installs the Azure DevOps Shell shortcut and Windows Terminal profiles
    /// </summary>
    [TfsCmdlet(CmdletScope.None)]
    partial class InstallShell
    {
        /// <summary>
        /// Specifies the installation target(s). Valid options are: WindowsTerminal, StartMenu, Taskbar, Desktop. 
        /// If omitted, defaults to all available targets based on Windows Terminal detection.
        /// </summary>
        [Parameter]
        public string[] Target { get; set; } = new string[] { "StartMenu", "Desktop" };
        
        /// <summary>
        /// Forces installation even if Windows Terminal is not detected. When set, creates PowerShell shortcuts instead.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController]
    partial class InstallShellController : ControllerBase
    {
        protected override IEnumerable Run()
        {
            var targets = Parameters.Get<string[]>("Target");
            var force = Parameters.Get<bool>("Force");
            
            Logger.Log("Installing Azure DevOps Shell shortcuts...");
            
            // Detect Windows Terminal
            bool hasWindowsTerminal = DetectWindowsTerminal();
            
            if (hasWindowsTerminal)
            {
                Logger.Log("Windows Terminal detected. Installing Windows Terminal profiles and shortcuts.");
                InstallWindowsTerminalProfiles();
                CreateWindowsTerminalShortcuts(targets);
            }
            else if (force)
            {
                Logger.Log("Windows Terminal not detected, but Force specified. Installing PowerShell shortcuts.");
                CreatePowerShellShortcuts(targets);
            }
            else
            {
                Logger.Log("Windows Terminal not detected. Installing PowerShell shortcuts.");
                CreatePowerShellShortcuts(targets);
            }
            
            PowerShell.WriteObject("Azure DevOps Shell installation completed successfully.");
            return null;
        }
        
        protected override void CacheParameters()
        {
            // No parameters to cache
        }
        
        private bool DetectWindowsTerminal()
        {
            try
            {
                // Check for wt.exe in standard locations
                var wtPaths = new[]
                {
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                                "Microsoft", "WindowsApps", "wt.exe"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), 
                                "WindowsApps", "Microsoft.WindowsTerminal_*", "wt.exe")
                };
                
                foreach (var path in wtPaths)
                {
                    if (path.Contains("*"))
                    {
                        var directory = Path.GetDirectoryName(path);
                        var fileName = Path.GetFileName(path);
                        if (Directory.Exists(directory))
                        {
                            var matches = Directory.GetDirectories(directory, "Microsoft.WindowsTerminal_*");
                            foreach (var match in matches)
                            {
                                if (File.Exists(Path.Combine(match, "wt.exe")))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    else if (File.Exists(path))
                    {
                        return true;
                    }
                }
                
                // Check registry AppPaths
                try
                {
                    using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\wt.exe"))
                    {
                        if (key != null)
                        {
                            var wtPath = key.GetValue("") as string;
                            if (!string.IsNullOrEmpty(wtPath) && File.Exists(wtPath))
                            {
                                return true;
                            }
                        }
                    }
                }
                catch
                {
                    // Registry access might fail, continue with file-based detection
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Error detecting Windows Terminal: {ex.Message}");
                return false;
            }
        }
        
        private void InstallWindowsTerminalProfiles()
        {
            try
            {
                var settingsPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Packages", "Microsoft.WindowsTerminal_8wekyb3d8bbwe", "LocalState", "profiles");
                    
                if (!Directory.Exists(settingsPath))
                {
                    Directory.CreateDirectory(settingsPath);
                }
                
                var moduleBase = PowerShell.Module.ModuleBase;
                var winPSProfile = Path.Combine(moduleBase, "AzureDevOpsShell-WinPS.json");
                var psCoreProfile = Path.Combine(moduleBase, "AzureDevOpsShell-PSCore.json");
                
                if (File.Exists(winPSProfile))
                {
                    var destPath = Path.Combine(settingsPath, "AzureDevOpsShell-WinPS.json");
                    File.Copy(winPSProfile, destPath, true);
                    Logger.Log($"Windows PowerShell profile installed to: {destPath}");
                }
                
                if (File.Exists(psCoreProfile))
                {
                    var destPath = Path.Combine(settingsPath, "AzureDevOpsShell-PSCore.json");
                    File.Copy(psCoreProfile, destPath, true);
                    Logger.Log($"PowerShell Core profile installed to: {destPath}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Failed to install Windows Terminal profiles: {ex.Message}");
            }
        }
        
        private void CreateWindowsTerminalShortcuts(string[] targets)
        {
            foreach (var target in targets)
            {
                try
                {
                    switch (target.ToLowerInvariant())
                    {
                        case "startmenu":
                            CreateWindowsTerminalShortcut(Environment.SpecialFolder.StartMenu, "Programs");
                            break;
                        case "desktop":
                            CreateWindowsTerminalShortcut(Environment.SpecialFolder.DesktopDirectory);
                            break;
                        default:
                            Logger.LogWarning($"Unknown target: {target}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Failed to create shortcut for target '{target}': {ex.Message}");
                }
            }
        }
        
        private void CreatePowerShellShortcuts(string[] targets)
        {
            foreach (var target in targets)
            {
                try
                {
                    switch (target.ToLowerInvariant())
                    {
                        case "startmenu":
                            CreatePowerShellShortcut(Environment.SpecialFolder.StartMenu, "Programs");
                            break;
                        case "desktop":
                            CreatePowerShellShortcut(Environment.SpecialFolder.DesktopDirectory);
                            break;
                        default:
                            Logger.LogWarning($"Unknown target: {target}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Failed to create shortcut for target '{target}': {ex.Message}");
                }
            }
        }
        
        private void CreateWindowsTerminalShortcut(Environment.SpecialFolder folder, string subfolder = null)
        {
            var folderPath = Environment.GetFolderPath(folder);
            if (!string.IsNullOrEmpty(subfolder))
            {
                folderPath = Path.Combine(folderPath, subfolder);
            }
            
            var shortcutPath = Path.Combine(folderPath, "Azure DevOps Shell.lnk");
            CreateShortcut(shortcutPath, "wt.exe", "-p \"Azure DevOps Shell (Windows PowerShell)\"");
        }
        
        private void CreatePowerShellShortcut(Environment.SpecialFolder folder, string subfolder = null)
        {
            var folderPath = Environment.GetFolderPath(folder);
            if (!string.IsNullOrEmpty(subfolder))
            {
                folderPath = Path.Combine(folderPath, subfolder);
            }
            
            var shortcutPath = Path.Combine(folderPath, "Azure DevOps Shell.lnk");
            var powershellPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), 
                                            "WindowsPowerShell", "v1.0", "powershell.exe");
            var arguments = "-NoExit -Command \"Import-Module TfsCmdlets; Enter-TfsShell\"";
            
            CreateShortcut(shortcutPath, powershellPath, arguments);
        }
        
        private void CreateShortcut(string shortcutPath, string targetPath, string arguments)
        {
            try
            {
                var directory = Path.GetDirectoryName(shortcutPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Use PowerShell to create the shortcut since it's more reliable cross-platform
                var script = $@"
                    $WshShell = New-Object -comObject WScript.Shell
                    $Shortcut = $WshShell.CreateShortcut('{shortcutPath.Replace("'", "''")}')
                    $Shortcut.TargetPath = '{targetPath.Replace("'", "''")}'
                    $Shortcut.Arguments = '{arguments.Replace("'", "''")}'
                    $Shortcut.WorkingDirectory = '$env:USERPROFILE'
                    $Shortcut.IconLocation = '{Path.Combine(PowerShell.Module.ModuleBase, "TfsCmdletsShell.ico").Replace("'", "''")}'
                    $Shortcut.Save()
                ";
                
                PowerShell.InvokeScript(script);
                Logger.Log($"Shortcut created: {shortcutPath}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to create shortcut '{shortcutPath}': {ex.Message}");
            }
        }

        public InstallShellController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger) 
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}