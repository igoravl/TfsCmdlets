//HintName: TfsCmdlets.Cmdlets.Team.Backlog.GetTeamBacklogLevelController.g.cs
using System.Management.Automation;
using WebApiBacklogLevelConfiguration = Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
namespace TfsCmdlets.Cmdlets.Team.Backlog
{
    internal partial class GetTeamBacklogLevelController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkHttpClient Client { get; }
        // Backlog
        protected bool Has_Backlog => Parameters.HasParameter(nameof(Backlog));
        protected IEnumerable Backlog
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Backlog), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Team
        protected bool Has_Team => Parameters.HasParameter("Team");
        protected WebApiTeam Team => Data.GetTeam();
        // Project
        protected bool Has_Project => Parameters.HasParameter("Project");
        protected WebApiTeamProject Project => Data.GetProject();
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.BacklogLevelConfiguration);
        protected override void CacheParameters()
        {
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetTeamBacklogLevelController(TfsCmdlets.HttpClients.IWorkHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}