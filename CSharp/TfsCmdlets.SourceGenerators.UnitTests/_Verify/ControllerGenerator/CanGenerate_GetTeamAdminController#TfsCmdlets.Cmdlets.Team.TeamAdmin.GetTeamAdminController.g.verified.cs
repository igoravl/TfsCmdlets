//HintName: TfsCmdlets.Cmdlets.Team.TeamAdmin.GetTeamAdminController.g.cs
using System.Management.Automation;
using TfsCmdlets.Util;
namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    internal partial class GetTeamAdminController: ControllerBase
    {
        // Admin
        protected bool Has_Admin { get; set; }
        protected string Admin { get; set; }
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
        public override Type DataType => typeof(TfsCmdlets.Models.TeamAdmin);
        protected override void CacheParameters()
        {
            // Admin
            Has_Admin = Parameters.HasParameter("Admin");
            Admin = Parameters.Get<string>("Admin", "*");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetTeamAdminController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}