using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{

    /// <summary>
    /// Gets one of more team project collections (organizations in Azure DevOps).
    /// </summary>
    [TfsCmdlet(CmdletScope.Server, DefaultParameterSetName = "Get by collection", 
#if NETCOREAPP3_1_OR_GREATER    
     OutputType = typeof(Microsoft.VisualStudio.Services.WebApi.VssConnection))]
#else
     OutputType = typeof(Microsoft.TeamFoundation.Client.TfsTeamProjectCollection))]
#endif
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
        [Alias("Organization")]
        [SupportsWildcards]
        public object Collection { get; set; }

        /// <summary>
        /// Returns the team project collection specified in the last call to 
        /// Connect-TfsTeamProjectCollection (i.e. the "current" project collection)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }
    }

    [CmdletController(typeof(Connection))]
    partial class GetTeamProjectCollectionController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            var current = Parameters.Get<bool>("Current");

            if (current)
            {
                yield return CurrentConnections.Collection;
                yield break;
            }

            var colsObj = Parameters.HasParameter("Organization") ?
                Parameters.Get<object>("Organization") :
                Parameters.Get<object>("Collection");

            IEnumerable cols;

            if (colsObj is ICollection enumObj)
            {
                cols = enumObj;
            }
            else
            {
                cols = new[] { colsObj };
            }

            foreach (var col in cols)
            {
                yield return Data.GetCollection(new { Collection = col });
            }
        }
    }
}