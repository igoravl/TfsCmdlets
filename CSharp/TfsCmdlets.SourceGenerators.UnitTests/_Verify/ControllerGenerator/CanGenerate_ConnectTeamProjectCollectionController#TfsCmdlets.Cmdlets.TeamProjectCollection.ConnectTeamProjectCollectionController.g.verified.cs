//HintName: TfsCmdlets.Cmdlets.TeamProjectCollection.ConnectTeamProjectCollectionController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ClientNotification;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    internal partial class ConnectTeamProjectCollectionController: ControllerBase
    {
        // Collection
        protected bool Has_Collection { get; set; }
        protected object Collection { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
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
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // Items
        protected IEnumerable<TfsCmdlets.Models.Connection> Items => Collection switch {
            TfsCmdlets.Models.Connection item => new[] { item },
            IEnumerable<TfsCmdlets.Models.Connection> items => items,
            _ => Data.GetItems<TfsCmdlets.Models.Connection>()
        };
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.Connection);
        protected override void CacheParameters()
        {
            // Collection
            Has_Collection = Parameters.HasParameter("Collection");
            Collection = Parameters.Get<object>("Collection");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
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
        public ConnectTeamProjectCollectionController(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}