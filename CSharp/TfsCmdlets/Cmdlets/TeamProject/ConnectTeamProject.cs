using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Connects to a Team Project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, DefaultParameterSetName = "Prompt for credential", OutputType = typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject))]
    partial class ConnectTeamProject
    {
        /// <summary>
        /// Specifies the name of the Team Project, its ID (a GUID), or a 
        /// Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public object Project { get; set; }
    }

    [CmdletController(typeof(WebApiTeamProject))]
    partial class ConnectTeamProjectController
    {
        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();

            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp);

            Logger.Log($"Connected to '{tp.Name}'");

            yield return tp;
        }

        [Import]
        private ICurrentConnections CurrentConnections { get; }
    }
}