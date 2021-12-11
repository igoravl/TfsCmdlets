using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;

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
    [TfsCmdlet(CmdletScope.Server)]
    partial class GetTeamProjectCollection
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by collection")]
        [Parameter(Position = 0, ParameterSetName = "Cached credentials")]
        [Parameter(Position = 0, ParameterSetName = "User name and password")]
        [Parameter(Position = 0, ParameterSetName = "Credential object")]
        [Parameter(Position = 0, ParameterSetName = "Personal Access Token")]
        [Parameter(Position = 0, ParameterSetName = "Prompt for credential")]
        [SupportsWildcards]
        public object Collection { get; set; }

        /// <summary>
        /// Returns the team project collection specified in the last call to 
        /// Connect-TfsTeamProjectCollection (i.e. the "current" project collection)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }
    }
}