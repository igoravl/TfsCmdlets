using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    /// <summary>
    /// <para type="synopsis">
    /// Gets one or more Team Foundation Server addresses registered in the current computer.
    /// </para>
    /// <para type="description">
    /// 
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "RegisteredConfigurationServer")]
    [OutputType("Microsoft.TeamFoundation.Client.RegisteredConfigurationServer")]
    [WindowsOnly]
    public partial class GetRegisteredConfigurationServer : BaseCmdlet
    {
        /// <summary>
        /// <para type="description">
        /// Specifies the name of a registered server. When omitted, all registered servers are returned. Wildcards are permitted.
        /// </para>
        /// </summary>
        /// <value></value>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Server { get; set; } = "*";

        partial void DoProcessRecord();

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            DoProcessRecord();
        }
    }
}
