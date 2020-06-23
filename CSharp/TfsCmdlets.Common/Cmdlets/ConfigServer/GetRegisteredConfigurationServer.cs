using System.Management.Automation;

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
    }
}