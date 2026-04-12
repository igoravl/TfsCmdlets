//HintName: TfsCmdlets.Cmdlets.Team.TeamMember.RemoveTeamMemberController.g.cs
using System.Management.Automation;
namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    internal partial class RemoveTeamMemberController: ControllerBase
    {
        // Member
        protected bool Has_Member { get; set; }
        protected object Member { get; set; }
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
        protected IEnumerable<TfsCmdlets.Models.Identity> Items => Member switch {
            TfsCmdlets.Models.Identity item => new[] { item },
            IEnumerable<TfsCmdlets.Models.Identity> items => items,
            _ => Data.GetItems<TfsCmdlets.Models.Identity>()
        };
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.Identity);
        protected override void CacheParameters()
        {
            // Member
            Has_Member = Parameters.HasParameter("Member");
            Member = Parameters.Get<object>("Member");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveTeamMemberController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}