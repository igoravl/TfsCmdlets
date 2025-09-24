//HintName: TfsCmdlets.Cmdlets.TeamProject.GetTeamProjectController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.Cmdlets.TeamProject
{
    internal partial class GetTeamProjectController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IProjectHttpClient Client { get; }
        // Project
        protected bool Has_Project => Parameters.HasParameter(nameof(Project));
        protected IEnumerable Project
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Project), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Deleted
        protected bool Has_Deleted { get; set; }
        protected bool Deleted { get; set; }
        // Process
        protected bool Has_Process { get; set; }
        protected string[] Process { get; set; }
        // IncludeDetails
        protected bool Has_IncludeDetails { get; set; }
        protected bool IncludeDetails { get; set; }
        // Current
        protected bool Has_Current { get; set; }
        protected bool Current { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject);
        protected override void CacheParameters()
        {
            // Deleted
            Has_Deleted = Parameters.HasParameter("Deleted");
            Deleted = Parameters.Get<bool>("Deleted");
            // Process
            Has_Process = Parameters.HasParameter("Process");
            Process = Parameters.Get<string[]>("Process");
            // IncludeDetails
            Has_IncludeDetails = Parameters.HasParameter("IncludeDetails");
            IncludeDetails = Parameters.Get<bool>("IncludeDetails");
            // Current
            Has_Current = Parameters.HasParameter("Current");
            Current = Parameters.Get<bool>("Current");
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
        public GetTeamProjectController(ICurrentConnections currentConnections, IPaginator paginator, TfsCmdlets.HttpClients.IProjectHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            CurrentConnections = currentConnections;
            Paginator = paginator;
            Client = client;
        }
    }
}