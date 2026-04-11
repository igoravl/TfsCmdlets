//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.GetPersonalAccessTokenController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.TokenAdmin.Client;
using Microsoft.VisualStudio.Services.WebApi;
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    internal partial class GetPersonalAccessTokenController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITokensHttpClient Client { get; }
        // PersonalAccessToken
        protected bool Has_PersonalAccessToken => Parameters.HasParameter(nameof(PersonalAccessToken));
        protected IEnumerable PersonalAccessToken
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(PersonalAccessToken), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // AuthorizationId
        protected bool Has_AuthorizationId { get; set; }
        protected System.Guid AuthorizationId { get; set; }
        // State
        protected bool Has_State { get; set; }
        protected Microsoft.VisualStudio.Services.DelegatedAuthorization.DisplayFilterOptions State { get; set; }
        // SortBy
        protected bool Has_SortBy { get; set; }
        protected Microsoft.VisualStudio.Services.DelegatedAuthorization.SortByOptions SortBy { get; set; }
        // Descending
        protected bool Has_Descending { get; set; }
        protected bool Descending { get; set; }
        // User
        protected bool Has_User { get; set; }
        protected object User { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken);
        protected override void CacheParameters()
        {
            // AuthorizationId
            Has_AuthorizationId = Parameters.HasParameter("AuthorizationId");
            AuthorizationId = Parameters.Get<System.Guid>("AuthorizationId");
            // State
            Has_State = Parameters.HasParameter("State");
            State = Parameters.Get<Microsoft.VisualStudio.Services.DelegatedAuthorization.DisplayFilterOptions>("State", DisplayFilterOptions.Active);
            // SortBy
            Has_SortBy = Parameters.HasParameter("SortBy");
            SortBy = Parameters.Get<Microsoft.VisualStudio.Services.DelegatedAuthorization.SortByOptions>("SortBy", SortByOptions.DisplayName);
            // Descending
            Has_Descending = Parameters.HasParameter("Descending");
            Descending = Parameters.Get<bool>("Descending");
            // User
            Has_User = Parameters.HasParameter("User");
            User = Parameters.Get<object>("User");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetPersonalAccessTokenController(ITokenAdminHttpClient tokenAdminClient, TfsCmdlets.HttpClients.ITokensHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            TokenAdminClient = tokenAdminClient;
            Client = client;
        }
    }
}