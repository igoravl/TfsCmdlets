namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Uninstalls the Azure DevOps Shell shortcut and Windows Terminal profiles
    /// </summary>
    [TfsCmdlet(CmdletScope.None)]
    partial class UninstallShell
    {
        /// <summary>
        /// Specifies the installation target(s) to remove. Valid options are: WindowsTerminal, StartMenu, Taskbar, Desktop. 
        /// If omitted, removes from all known locations.
        /// </summary>
        [Parameter]
        public string[] Target { get; set; } = new string[] { "StartMenu", "Desktop", "WindowsTerminal" };
    }

    [CmdletController]
    partial class UninstallShellController : ControllerBase
    {
        protected override IEnumerable Run()
        {
            var targets = Parameters.Get<string[]>("Target");
            
            Logger.Log("Uninstalling Azure DevOps Shell shortcuts and profiles...");
            
            foreach (var target in targets)
            {
                try
                {
                    switch (target.ToLowerInvariant())
                    {
                        case "startmenu":
                            RemoveShortcut(Environment.SpecialFolder.StartMenu, "Programs");
                            break;
                        case "desktop":
                            RemoveShortcut(Environment.SpecialFolder.DesktopDirectory);
                            break;
                        case "windowsterminal":
                            RemoveWindowsTerminalProfiles();
                            break;
                        default:
                            Logger.LogWarning($"Unknown target: {target}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Failed to remove target '{target}': {ex.Message}");
                }
            }
            
            PowerShell.WriteObject("Azure DevOps Shell uninstallation completed successfully.");
            return null;
        }
        
        protected override void CacheParameters()
        {
            // No parameters to cache
        }
        
        private void RemoveShortcut(Environment.SpecialFolder folder, string subfolder = null)
        {
            try
            {
                var folderPath = Environment.GetFolderPath(folder);
                if (!string.IsNullOrEmpty(subfolder))
                {
                    folderPath = Path.Combine(folderPath, subfolder);
                }
                
                var shortcutPath = Path.Combine(folderPath, "Azure DevOps Shell.lnk");
                
                if (File.Exists(shortcutPath))
                {
                    File.Delete(shortcutPath);
                    Logger.Log($"Shortcut removed: {shortcutPath}");
                }
                else
                {
                    Logger.Log($"Shortcut not found: {shortcutPath}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to remove shortcut from {folder}: {ex.Message}");
            }
        }
        
        private void RemoveWindowsTerminalProfiles()
        {
            try
            {
                var settingsPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Packages", "Microsoft.WindowsTerminal_8wekyb3d8bbwe", "LocalState", "profiles");
                
                var profiles = new[]
                {
                    Path.Combine(settingsPath, "AzureDevOpsShell-WinPS.json"),
                    Path.Combine(settingsPath, "AzureDevOpsShell-PSCore.json")
                };
                
                foreach (var profile in profiles)
                {
                    if (File.Exists(profile))
                    {
                        File.Delete(profile);
                        Logger.Log($"Windows Terminal profile removed: {profile}");
                    }
                    else
                    {
                        Logger.Log($"Windows Terminal profile not found: {profile}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to remove Windows Terminal profiles: {ex.Message}");
            }
        }

        public UninstallShellController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger) 
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}