//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.RenamePersonalAccessTokenController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.WebApi.Contracts.DelegatedAuthorization;
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    internal partial class RenamePersonalAccessTokenController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITokensHttpClient Client { get; }
        // PersonalAccessToken
        protected bool Has_PersonalAccessToken { get; set; }
        protected object PersonalAccessToken { get; set; }
        // NewName
        protected bool Has_NewName { get; set; }
        protected string NewName { get; set; }
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
            // NewName
            Has_NewName = Parameters.HasParameter("NewName");
            NewName = Parameters.Get<string>("NewName");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RenamePersonalAccessTokenController(TfsCmdlets.HttpClients.ITokensHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}