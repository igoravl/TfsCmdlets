using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.Admin
{
    [CmdletController(typeof(TfsInstallationPath))]
    partial class GetInstallationPathController
    {
        [Import]
        private IRegistryService Registry { get; }

        [Import]
        private ITfsVersionTable TfsVersionTable { get; }

        protected override IEnumerable Run()
        {
            if (Has_Session)
            {
                throw new NotImplementedException("Remote sessions are currently not supported");
            }

            if (!Parameters.Get<string>("ComputerName").Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                throw new NotImplementedException("Remote computers are currently not supported");
            }

            var computerName = Parameters.Get<string>("ComputerName");
            var versionNumber = Parameters.Get<int>("Version");
            var component = Parameters.Get<TfsComponent>("Component");

            string version = null;

            if (versionNumber == 0)
            {
                Logger.Log("TFS version not specified. Trying to detect installed version.");

                for (var i = 20; i > 8; i--)
                {
                    if (!Registry.HasValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\TeamFoundationServer\{i}.0", "InstallPath"))
                        continue;

                    Logger.Log($"Found TFS {TfsVersionTable.GetYear(i)} (version {i}.0).");
                    version = $"{i}.0";
                    break;
                }

                if (version == null)
                {
                    Logger.LogError(new Exception($"No Team Foundation Server installation found in computer {Environment.MachineName}."));
                    return null;
                }
            }
            else
            {
                version = $"{TfsVersionTable.GetMajorVersion(versionNumber)}.0";

                Logger.Log($"Searching for installed TFS {versionNumber} (version {version}).");
            }

            var rootKeyPath = $@"HKEY_LOCAL_MACHINE\Software\Microsoft\TeamFoundationServer\{version}";

            var componentPath = component switch
            {
                TfsComponent.BaseInstallation => rootKeyPath,
                _ => $@"{rootKeyPath}\InstalledComponents\{component}"
            };

            string result;

            if (!Registry.HasValue(rootKeyPath, "InstallPath"))
            {
                Logger.LogError(new Exception($"Team Foundation Server is not installed in computer {Environment.MachineName}"));
                return null;
            }

            if ((result = (string) Registry.GetValue(componentPath, "InstallPath")) == null)
            {
                Logger.LogError(new Exception($"Team Foundation Server component '{component}' is not installed in computer {Environment.MachineName}"));
                return null;
            }

            return new[] { new TfsInstallationPath(result) };
        }
    }
}