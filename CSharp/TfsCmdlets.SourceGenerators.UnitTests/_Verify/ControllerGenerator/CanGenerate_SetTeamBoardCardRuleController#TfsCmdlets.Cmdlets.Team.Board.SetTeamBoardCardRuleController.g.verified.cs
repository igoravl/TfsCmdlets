//HintName: TfsCmdlets.Cmdlets.Team.Board.SetTeamBoardCardRuleController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
namespace TfsCmdlets.Cmdlets.Team.Board
{
    internal partial class SetTeamBoardCardRuleController: ControllerBase
    {
        // WebApiBoard
        protected bool Has_WebApiBoard { get; set; }
        protected object WebApiBoard { get; set; }
        // Rules
        protected bool Has_Rules { get; set; }
        protected Microsoft.TeamFoundation.Work.WebApi.BoardCardRuleSettings Rules { get; set; }
        // CardStyleRuleName
        protected bool Has_CardStyleRuleName { get; set; }
        protected string CardStyleRuleName { get; set; }
        // CardStyleRuleFilter
        protected bool Has_CardStyleRuleFilter { get; set; }
        protected string CardStyleRuleFilter { get; set; }
        // CardStyleRuleSettings
        protected bool Has_CardStyleRuleSettings { get; set; }
        protected System.Collections.Hashtable CardStyleRuleSettings { get; set; }
        // TagStyleRuleName
        protected bool Has_TagStyleRuleName { get; set; }
        protected string TagStyleRuleName { get; set; }
        // TagStyleRuleFilter
        protected bool Has_TagStyleRuleFilter { get; set; }
        protected string TagStyleRuleFilter { get; set; }
        // TagStyleRuleSettings
        protected bool Has_TagStyleRuleSettings { get; set; }
        protected System.Collections.Hashtable TagStyleRuleSettings { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
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
        // Items
        protected IEnumerable<TfsCmdlets.Models.CardRule> Items => WebApiBoard switch {
            TfsCmdlets.Models.CardRule item => new[] { item },
            IEnumerable<TfsCmdlets.Models.CardRule> items => items,
            _ => Data.GetItems<TfsCmdlets.Models.CardRule>()
        };
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.CardRule);
        protected override void CacheParameters()
        {
            // WebApiBoard
            Has_WebApiBoard = Parameters.HasParameter("WebApiBoard");
            WebApiBoard = Parameters.Get<object>("WebApiBoard");
            // Rules
            Has_Rules = Parameters.HasParameter("Rules");
            Rules = Parameters.Get<Microsoft.TeamFoundation.Work.WebApi.BoardCardRuleSettings>("Rules");
            // CardStyleRuleName
            Has_CardStyleRuleName = Parameters.HasParameter("CardStyleRuleName");
            CardStyleRuleName = Parameters.Get<string>("CardStyleRuleName");
            // CardStyleRuleFilter
            Has_CardStyleRuleFilter = Parameters.HasParameter("CardStyleRuleFilter");
            CardStyleRuleFilter = Parameters.Get<string>("CardStyleRuleFilter");
            // CardStyleRuleSettings
            Has_CardStyleRuleSettings = Parameters.HasParameter("CardStyleRuleSettings");
            CardStyleRuleSettings = Parameters.Get<System.Collections.Hashtable>("CardStyleRuleSettings");
            // TagStyleRuleName
            Has_TagStyleRuleName = Parameters.HasParameter("TagStyleRuleName");
            TagStyleRuleName = Parameters.Get<string>("TagStyleRuleName");
            // TagStyleRuleFilter
            Has_TagStyleRuleFilter = Parameters.HasParameter("TagStyleRuleFilter");
            TagStyleRuleFilter = Parameters.Get<string>("TagStyleRuleFilter");
            // TagStyleRuleSettings
            Has_TagStyleRuleSettings = Parameters.HasParameter("TagStyleRuleSettings");
            TagStyleRuleSettings = Parameters.Get<System.Collections.Hashtable>("TagStyleRuleSettings");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public SetTeamBoardCardRuleController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}