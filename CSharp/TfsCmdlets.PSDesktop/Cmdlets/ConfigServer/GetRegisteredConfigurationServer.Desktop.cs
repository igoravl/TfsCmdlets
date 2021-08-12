using System;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    partial class GetRegisteredConfigurationServer
    {
        /// <inheritdoc/>
        protected override void DoProcessRecord()
        {
            if (Server.Equals("localhost", StringComparison.OrdinalIgnoreCase) || Server.Equals("."))
            {
               Server = Environment.MachineName;
            }

            WriteObject(RegisteredTfsConnections.GetConfigurationServers().Where(s => s.Name.IsLike(Server)), true);
        }
    }
}
