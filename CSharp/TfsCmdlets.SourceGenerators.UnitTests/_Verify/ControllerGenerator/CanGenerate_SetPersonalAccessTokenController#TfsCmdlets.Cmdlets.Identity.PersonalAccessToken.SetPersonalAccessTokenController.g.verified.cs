//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.SetPersonalAccessTokenController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.WebApi.Contracts.DelegatedAuthorization;
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    internal partial class SetPersonalAccessTokenController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITokensHttpClient Client { get; }
        // PersonalAccessToken
        protected bool Has_PersonalAccessToken { get; set; }
        protected object PersonalAccessToken { get; set; }
        // Scope
        protected bool Has_Scope { get; set; }
        protected string[] Scope { get; set; }
        // ValidTo
        protected bool Has_ValidTo { get; set; }
        protected System.DateTime ValidTo { get; set; }
        // AllOrganizations
        protected bool Has_AllOrganizations { get; set; }
        protected bool AllOrganizations { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
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
        protected IEnumerable<Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken> Items => PersonalAccessToken switch {
            Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken item => new[] { item },
            IEnumerable<Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken> items => items,
            _ => Data.GetItems<Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken);
        protected override void CacheParameters()
        {
            // PersonalAccessToken
            Has_PersonalAccessToken = Parameters.HasParameter("PersonalAccessToken");
            PersonalAccessToken = Parameters.Get<object>("PersonalAccessToken");
            // Scope
            Has_Scope = Parameters.HasParameter("Scope");
            Scope = Parameters.Get<string[]>("Scope");
            // ValidTo
            Has_ValidTo = Parameters.HasParameter("ValidTo");
            ValidTo = Parameters.Get<System.DateTime>("ValidTo");
            // AllOrganizations
            Has_AllOrganizations = Parameters.HasParameter("AllOrganizations");
            AllOrganizations = Parameters.Get<bool>("AllOrganizations");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public SetPersonalAccessTokenController(TfsCmdlets.HttpClients.ITokensHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}