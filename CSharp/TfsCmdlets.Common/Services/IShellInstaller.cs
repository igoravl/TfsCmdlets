namespace TfsCmdlets.Services
{
    public interface IShellInstallerFactory
    {
        IEnumerable<IShellInstaller> Create(ShellInstallationTarget target);
    }

    public interface IShellInstaller
    {
        string DisplayName { get; }
        
        bool Install(ShellInstallationScope scope, bool force);

        bool Uninstall(ShellInstallationScope scope);
    }
}