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
    [WindowsOnly]
    public partial class GetRegisteredConfigurationServer : BaseCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Server { get; set; } = "*";

        partial void DoProcessRecord();

        protected override void ProcessRecord()
        {
            DoProcessRecord();
        }
    }
}
