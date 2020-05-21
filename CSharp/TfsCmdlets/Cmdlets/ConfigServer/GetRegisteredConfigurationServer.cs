/*
.SYNOPSIS
    Gets one or more Team Foundation Server addresses registered in the current computer.

.PARAMETER Name
    Specifies the name of a registered server. When omitted, all registered servers are returned. Wildcards are permitted.

.INPUTS
    System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    [Cmdlet(VerbsCommon.Get, "RegisteredConfigurationServer")]
    [OutputType("Microsoft.TeamFoundation.Client.RegisteredConfigurationServer")]
    public class GetRegisteredConfigurationServer: PSCmdlet
    {
/*
        [Parameter(Position=0, ValueFromPipeline=true)]
        [Alias("Name")]
        public string Server { get; set; } = "*"

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Client"
    }

    protected override void ProcessRecord()
    {
        if((Server = = $"localhost") || ({Server} == "."))
        {
            Server = env:COMPUTERNAME
        }

        WriteObject([Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetConfigurationServers() | Where-Object Name -Like Server); return;
    }
}
*/
}
}
