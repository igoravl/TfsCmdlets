using TfsCmdlets.Services;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IShellInstallerFactory))]
    internal class ShellInstallerFactoryImpl : IShellInstallerFactory
    {
        private static readonly IDictionary<ShellInstallationTarget, Func<IShellInstaller>> _installers = new Dictionary<ShellInstallationTarget, Func<IShellInstaller>>
        {
            { ShellInstallationTarget.StartMenu, () => new StartMenuShellInstaller() },
            { ShellInstallationTarget.WindowsTerminal, () => new WindowsTerminalShellInstaller() }
        };

        public IEnumerable<IShellInstaller> Create(ShellInstallationTarget targets)
        {
            var installers = Enum.GetValues(typeof(ShellInstallationTarget))
                .Cast<ShellInstallationTarget>()
                .Where(t => (t != ShellInstallationTarget.Auto) && (targets & t) == t)
                .Select(t => _installers[t]())
                .ToList();

            return installers;
        }

        private class StartMenuShellInstaller : IShellInstaller
        {
            public string DisplayName => "Start Menu Shortcut";

            public bool Install(ShellInstallationScope scope, bool force)
            {
                throw new NotImplementedException();
            }

            public bool Uninstall(ShellInstallationScope scope)
            {
                throw new NotImplementedException();
            }
        }

        private class WindowsTerminalShellInstaller : IShellInstaller
        {
            public string DisplayName => "Windows Terminal Profile";

            public bool Install(ShellInstallationScope scope, bool force)
            {
                try
                {
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            public bool Uninstall(ShellInstallationScope scope)
            {
                try
                {
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
