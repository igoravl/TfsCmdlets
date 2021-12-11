using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    /// <summary>
    /// Gets one or more Team Foundation Server addresses registered in the current computer.
    /// </summary>
    [TfsCmdlet(CmdletScope.None, DesktopOnly = true)]
#if NET471_OR_GREATER
    , OutputType = typeof(Microsoft.TeamFoundation.Client.RegisteredConfigurationServer))]
#endif
    partial class GetRegisteredConfigurationServer
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