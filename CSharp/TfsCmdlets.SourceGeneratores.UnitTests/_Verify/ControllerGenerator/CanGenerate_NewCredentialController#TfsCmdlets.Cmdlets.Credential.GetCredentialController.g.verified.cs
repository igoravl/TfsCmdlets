//HintName: TfsCmdlets.Cmdlets.Credential.GetCredentialController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Common;
using System.Net;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.OAuth;
namespace TfsCmdlets.Cmdlets.Credential
{
    internal partial class GetCredentialController: ControllerBase
    {
        // Url
        protected bool Has_Url { get; set; }
        protected System.Uri Url { get; set; }
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
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Common.VssCredentials);
        protected override void CacheParameters()
        {
            // Url
            Has_Url = Parameters.HasParameter("Url");
            Url = Parameters.Get<System.Uri>("Url");
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
        public GetCredentialController(IInteractiveAuthentication interactiveAuthentication, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            InteractiveAuthentication = interactiveAuthentication;
        }
    }
}