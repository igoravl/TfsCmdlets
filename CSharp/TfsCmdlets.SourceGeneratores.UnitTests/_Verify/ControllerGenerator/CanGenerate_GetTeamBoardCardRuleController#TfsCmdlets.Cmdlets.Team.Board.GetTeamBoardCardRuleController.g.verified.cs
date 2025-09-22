//HintName: TfsCmdlets.Cmdlets.Team.Board.GetTeamBoardCardRuleController.g.cs
using System.Management.Automation;
using WebApiCardRule = Microsoft.TeamFoundation.Work.WebApi.Rule;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
namespace TfsCmdlets.Cmdlets.Team.Board
{
    internal partial class GetTeamBoardCardRuleController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkHttpClient Client { get; }
        // Rule
        protected bool Has_Rule => Parameters.HasParameter(nameof(Rule));
        protected IEnumerable Rule
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Rule), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // RuleType
        protected bool Has_RuleType { get; set; }
        protected TfsCmdlets.CardRuleType RuleType { get; set; }
        // Board
        protected bool Has_Board { get; set; }
        protected object Board { get; set; }
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
        public override Type DataType => typeof(TfsCmdlets.Models.CardRule);
        protected override void CacheParameters()
        {
            // RuleType
            Has_RuleType = Parameters.HasParameter("RuleType");
            RuleType = Parameters.Get<TfsCmdlets.CardRuleType>("RuleType", CardRuleType.All);
            // Board
            Has_Board = Parameters.HasParameter("Board");
            Board = Parameters.Get<object>("Board");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetTeamBoardCardRuleController(TfsCmdlets.HttpClients.IWorkHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}