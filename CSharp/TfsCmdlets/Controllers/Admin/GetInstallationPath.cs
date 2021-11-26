using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.Admin
{
    [CmdletController]
    internal class GetInstallationPath : ControllerBase<TfsInstallationPath>
    {
        private IRegistryService Registry { get; }
        private ITfsVersionTable TfsVersionTable { get; }

        public override IEnumerable<TfsInstallationPath> Invoke(ParameterDictionary parameters)
        {
            if (parameters.HasParameter("Session"))
            {
                throw new NotImplementedException("Remote sessions are currently not supported");
            }

            if (!parameters.Get<string>("ComputerName").Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                throw new NotImplementedException("Remote computers are currently not supported");
            }

            var computerName = parameters.Get<string>("ComputerName");
            var versionNumber = parameters.Get<int>("Version");
            var component = parameters.Get<TfsComponent>("Component");

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

        [ImportingConstructor]
        public GetInstallationPath(IRegistryService registry, ITfsVersionTable tfsVersionTable, IPowerShellService powerShell, IDataManager data, ILogger logger)
        : base(powerShell, data, logger)
        {
            Registry = registry;
            TfsVersionTable = tfsVersionTable;
        }
    }
}