using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
#if NET471_OR_GREATER
using Microsoft.TeamFoundation.Client;
#endif

    /// <summary>
    /// Gets one of more team project collections (organizations in Azure DevOps).
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamProjectCollection", DefaultParameterSetName = "Get by collection")]
#if NETCOREAPP3_1_OR_GREATER    
    [OutputType(typeof(VssConnection))]
#else
    [OutputType(typeof(TfsTeamProjectCollection))]
#endif
    partial class GetTeamProjectCollection
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

        // TODO

    }

}