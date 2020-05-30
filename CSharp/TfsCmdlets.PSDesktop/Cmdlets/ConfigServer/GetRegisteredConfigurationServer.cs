using System;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    /// <summary>
    /// Gets one or more Team Foundation Server addresses registered in the current computer.
    /// </summary>
    partial class GetRegisteredConfigurationServer
    {
        partial void DoProcessRecord()
        {
            if (Server.Equals("localhost", StringComparison.OrdinalIgnoreCase) || Server.Equals("."))
            {
               Server = Environment.MachineName;
            }

            WriteObject(RegisteredTfsConnections.GetConfigurationServers().Where(s => s.Name.IsLike(Server)), true);
        }
    }
}
