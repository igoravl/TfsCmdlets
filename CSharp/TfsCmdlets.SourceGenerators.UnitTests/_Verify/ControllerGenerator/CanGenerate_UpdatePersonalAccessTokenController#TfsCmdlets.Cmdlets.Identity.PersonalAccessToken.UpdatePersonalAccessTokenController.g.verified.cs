//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.UpdatePersonalAccessTokenController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    internal partial class UpdatePersonalAccessTokenController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITokensHttpClient Client { get; }
        // PersonalAccessToken
        protected bool Has_PersonalAccessToken { get; set; }
        protected object PersonalAccessToken { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
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
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public UpdatePersonalAccessTokenController(TfsCmdlets.HttpClients.ITokensHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}