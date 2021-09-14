using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
#if NET471_OR_GREATER
using Microsoft.TeamFoundation.Client;
#endif

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Gets one of more team project collections (organizations in Azure DevOps).
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamProjectCollection", DefaultParameterSetName = "Get by collection")]
#if NETCOREAPP3_1_OR_GREATER    
    [OutputType(typeof(VssConnection))]
#else
    [OutputType(typeof(TfsTeamProjectCollection))]
#endif
    public class GetTeamProjectCollection : CmdletBase<Models.TeamProjectCollection>
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by collection")]
        public new object Collection { get; set; }

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
    }
}

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
    [Controller(typeof(Models.TeamProjectCollection))]
    internal partial class TeamProjectCollectionController : ControllerBase<Models.TeamProjectCollection>
    {
        public ICurrentConnections CurrentConnections { get; private set; }

        public TeamProjectCollectionController(
            ICurrentConnections currentConnections,
            [InjectConnection(ClientScope.Collection)] Models.Connection collection,
            ILogger logger,
            IParameterManager parameterManager, 
            IPowerShellService powerShell)
            : base(collection, logger, parameterManager, powerShell)
        {
            CurrentConnections = currentConnections;
        }

        protected override IEnumerable<Models.TeamProjectCollection> DoGetItems(ParameterDictionary parameters)
        {
            var current = parameters.Get<bool>("Current");

            if (current)
            {
                yield return CurrentConnections.Collection;
                yield break;
            }

            yield return (Models.TeamProjectCollection)Collection;
        }
    }
}