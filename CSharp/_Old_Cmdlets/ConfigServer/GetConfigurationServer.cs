using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    // <para type="inputType">Microsoft.TeamFoundation.Client.TfsConfigurationServer
    // <para type="inputType">System.String
    // <para type="inputType">System.Uri

    /// <summary>
    ///   Gets information about a configuration server.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsConfigurationServer", DefaultParameterSetName = "Get by server")]
    public class GetConfigurationServer : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by server", ValueFromPipeline = true)]
        [AllowNull]
        public object Server { get; set; }

        /// <summary>
        /// Returns the configuration server specified in the last call to Connect-TfsConfigurationServer 
        /// (i.e. the "current" configuration server)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// HELP_PARAM_CREDENTIAL
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = "Get by server")]
        public object Credential { get; set; }

        // TODO

    }
}