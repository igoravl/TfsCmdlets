using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Gets one of more team project collections (organizations in Azure DevOps).
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamProjectCollection", DefaultParameterSetName = "Get by collection")]
    [OutputType(typeof(VssConnection))]
    public class GetTeamProjectCollection : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by collection")]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ValueFromPipeline = true, ParameterSetName = "Get by collection")]
        public object Server { get; set; }

        /// <summary>
        /// HELP_PARAM_CREDENTIAL
        /// </summary>
        [Parameter(ParameterSetName = "Get by collection")]
        public object Credential { get; set; }

        /// <summary>
        /// Returns the team project collection specified in the last call to 
        /// Connect-TfsTeamProjectCollection (i.e. the "current" project collection)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                WriteObject(this.GetItems<Models.Connection>(new {
                    ConnectionType = ClientScope.Collection
                }), true);
            }
            catch
            {
                if(!Current) throw;
            }
        }
    }
}