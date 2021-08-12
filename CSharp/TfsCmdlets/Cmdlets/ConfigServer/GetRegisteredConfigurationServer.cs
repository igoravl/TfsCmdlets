using System;
using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    /// <summary>
    /// Gets one or more Team Foundation Server addresses registered in the current computer.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsRegisteredConfigurationServer")]
    [OutputType("Microsoft.TeamFoundation.Client.RegisteredConfigurationServer")]
    [DesktopOnly]
    public partial class GetRegisteredConfigurationServer : CmdletBase
    {
        /// <summary>
        /// Specifies the name of a registered server. Wildcards are supported. 
        /// When omitted, all registered servers are returned. 
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Server { get; set; } = "*";

#if NET471_OR_GREATER
        /// <inheritdoc/>
        protected override void DoProcessRecord()
        {
            if (Server.Equals("localhost", StringComparison.OrdinalIgnoreCase) || Server.Equals("."))
            {
               Server = Environment.MachineName;
            }

            WriteObject(Microsoft.TeamFoundation.Client.RegisteredTfsConnections.GetConfigurationServers().Where(s => s.Name.IsLike(Server)), true);
        }
#endif
    }
}