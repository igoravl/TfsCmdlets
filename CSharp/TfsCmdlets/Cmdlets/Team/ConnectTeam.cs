using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Connects to a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Prompt for credential", OutputType = typeof(Models.Team))]
    partial class ConnectTeam
    {
        /// <summary>
        ///   Specifies the name of the Team, its ID (a GUID), or a 
        ///   Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. For more details, 
        ///   see the Get-TfsTeam cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public object Team { get; set; }

        /// <summary>
        /// Specifies that the credentials should be obtained from the currently logged in Azure CLI user.
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter AzCli { get; set; }

        /// <summary>
        /// Specifies that the credentials should be obtained from the Azure Managed Identity present in the current script context.
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter UseMSI { get; set; }
    }

    [CmdletController(typeof(Models.Team))]
    partial class ConnectTeamController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            var team = Data.GetItem<Models.Team>();

            CurrentConnections.Set(Collection.ConfigurationServer, Collection, Project, team);

            Logger.Log($"Connected to '{team.Name}'");

            yield return Team;
        }
    }
}
