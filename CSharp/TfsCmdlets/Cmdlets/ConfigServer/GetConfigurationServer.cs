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
    [TfsCmdlet(CmdletScope.None)]
    partial class GetConfigurationServer
    {
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Cached credentials")]
        [Parameter(Position = 0, ParameterSetName = "User name and password")]
        [Parameter(Position = 0, ParameterSetName = "Credential object")]
        [Parameter(Position = 0, ParameterSetName = "Personal Access Token")]
        [Parameter(Position = 0, ParameterSetName = "Prompt for credential")]
        public object Server { get; set; }

        /// <summary>
        /// Returns the configuration server specified in the last call to Connect-TfsConfigurationServer 
        /// (i.e. the "current" configuration server)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }
    }
}