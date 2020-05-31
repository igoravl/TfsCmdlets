using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    /// <summary>
    /// <para type="synopsis">
    /// Gets information about a configuration server.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "ConfigurationServer", DefaultParameterSetName = "Get by server")]
    [WindowsOnly]
    public partial class GetConfigurationServer : BaseCmdlet
    {
        /// <summary>
        /// <para type="description">
        /// HELP_SERVER
        /// </para>
        /// <para type="inputType">Microsoft.TeamFoundation.Client.TfsConfigurationServer</para>
        /// <para type="inputType">System.String</para>
        /// <para type="inputType">System.Uri</para>
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by server", ValueFromPipeline = true)]
        [AllowNull]
        public object Server { get; set; }

        /// <summary>
        /// <para type="description">
        /// Returns the configuration server specified in the last call to Connect-TfsConfigurationServer 
        /// (i.e. the "current" configuration server)
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// <para type="description">
        /// HELP_CREDENTIAL
        /// </para>
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = "Get by server")]
        public object Credential { get; set; }

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
