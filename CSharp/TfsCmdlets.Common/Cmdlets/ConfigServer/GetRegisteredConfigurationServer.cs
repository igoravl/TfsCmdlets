/*
.SYNOPSIS
    Gets one or more Team Foundation Server addresses registered in the current computer.

.PARAMETER Name
    Specifies the name of a registered server. When omitted, all registered servers are returned. Wildcards are permitted.

.INPUTS
    System.String
*/

using System;
using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    [Cmdlet(VerbsCommon.Get, "RegisteredConfigurationServer")]
    [OutputType("Microsoft.TeamFoundation.Client.RegisteredConfigurationServer")]
    public class GetRegisteredConfigurationServer : BaseCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Server { get; set; } = "*";

        protected override void ProcessRecord()
        {
#if NET462
            //if (Server.Equals("localhost", StringComparison.OrdinalIgnoreCase) || Server.Equals("."))
            //{
            //    Server = Environment.MachineName;
            //}

            //WriteObject(
            //    Microsoft.TeamFoundation.Client.RegisteredTfsConnections.GetConfigurationServers()
            //    .Where(s => s.Name.IsLike(Server)));
#else
            TfsCmdlets.Util.ErrorUtil.ThrowDesktopOnlyCmdlet();
#endif
        }
    }
}
