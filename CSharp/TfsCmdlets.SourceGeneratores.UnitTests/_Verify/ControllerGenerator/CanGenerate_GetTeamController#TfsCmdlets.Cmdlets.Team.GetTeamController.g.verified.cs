//HintName: TfsCmdlets.Cmdlets.Team.GetTeamController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
namespace TfsCmdlets.Cmdlets.Team
{
    internal partial class GetTeamController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITeamHttpClient Client { get; }
        // Team
        protected bool Has_Team => Parameters.HasParameter(nameof(Team));
        protected IEnumerable Team
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Team), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // IncludeMembers
        protected bool Has_IncludeMembers { get; set; }
        protected bool IncludeMembers { get; set; }
        // IncludeSettings
        protected bool Has_IncludeSettings { get; set; }
        protected bool IncludeSettings { get; set; }
        // Current
        protected bool Has_Current { get; set; }
        protected bool Current { get; set; }
        // Default
        protected bool Has_Default { get; set; }
        protected bool Default { get; set; }
        // Cached
        protected bool Has_Cached { get; set; }
        protected bool Cached { get; set; }
        // UserName
        protected bool Has_UserName { get; set; }
        protected string UserName { get; set; }
        // Password
        protected bool Has_Password { get; set; }
        protected System.Security.SecureString Password { get; set; }
        // Credential
        protected bool Has_Credential { get; set; }
        protected object Credential { get; set; }
        // PersonalAccessToken
        protected bool Has_PersonalAccessToken { get; set; }
        protected string PersonalAccessToken { get; set; }
        // Interactive
        protected bool Has_Interactive { get; set; }
        protected bool Interactive { get; set; }
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
        public override Type DataType => typeof(TfsCmdlets.Models.Team);
        protected override void CacheParameters()
        {
            // IncludeMembers
            Has_IncludeMembers = Parameters.HasParameter("IncludeMembers");
            IncludeMembers = Parameters.Get<bool>("IncludeMembers");
            // IncludeSettings
            Has_IncludeSettings = Parameters.HasParameter("IncludeSettings");
            IncludeSettings = Parameters.Get<bool>("IncludeSettings");
            // Current
            Has_Current = Parameters.HasParameter("Current");
            Current = Parameters.Get<bool>("Current");
            // Default
            Has_Default = Parameters.HasParameter("Default");
            Default = Parameters.Get<bool>("Default");
            // Cached
            Has_Cached = Parameters.HasParameter("Cached");
            Cached = Parameters.Get<bool>("Cached");
            // UserName
            Has_UserName = Parameters.HasParameter("UserName");
            UserName = Parameters.Get<string>("UserName");
            // Password
            Has_Password = Parameters.HasParameter("Password");
            Password = Parameters.Get<System.Security.SecureString>("Password");
            // Credential
            Has_Credential = Parameters.HasParameter("Credential");
            Credential = Parameters.Get<object>("Credential");
            // PersonalAccessToken
            Has_PersonalAccessToken = Parameters.HasParameter("PersonalAccessToken");
            PersonalAccessToken = Parameters.Get<string>("PersonalAccessToken");
            // Interactive
            Has_Interactive = Parameters.HasParameter("Interactive");
            Interactive = Parameters.Get<bool>("Interactive");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetTeamController(ICurrentConnections currentConnections, IPaginator paginator, IProjectHttpClient projectClient, IWorkHttpClient workClient, TfsCmdlets.HttpClients.ITeamHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            CurrentConnections = currentConnections;
            Paginator = paginator;
            ProjectClient = projectClient;
            WorkClient = workClient;
            Client = client;
        }
    }
}