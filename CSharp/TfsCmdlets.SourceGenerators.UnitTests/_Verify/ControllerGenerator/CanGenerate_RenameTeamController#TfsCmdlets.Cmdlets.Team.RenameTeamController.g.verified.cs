//HintName: TfsCmdlets.Cmdlets.Team.RenameTeamController.g.cs
using Microsoft.TeamFoundation.Core.WebApi;
using System.Management.Automation;
namespace TfsCmdlets.Cmdlets.Team
{
    internal partial class RenameTeamController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITeamHttpClient Client { get; }
        // Team
        protected bool Has_Team { get; set; }
        protected object Team { get; set; }
        // NewName
        protected bool Has_NewName { get; set; }
        protected string NewName { get; set; }
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
        // Items
        protected IEnumerable<TfsCmdlets.Models.Team> Items => Team switch {
            TfsCmdlets.Models.Team item => new[] { item },
            IEnumerable<TfsCmdlets.Models.Team> items => items,
            _ => Data.GetItems<TfsCmdlets.Models.Team>()
        };
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.Team);
        protected override void CacheParameters()
        {
            // Team
            Has_Team = Parameters.HasParameter("Team");
            Team = Parameters.Get<object>("Team");
            // NewName
            Has_NewName = Parameters.HasParameter("NewName");
            NewName = Parameters.Get<string>("NewName");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RenameTeamController(TfsCmdlets.HttpClients.ITeamHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}