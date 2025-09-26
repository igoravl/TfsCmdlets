//HintName: TfsCmdlets.Controllers.Identity.User.NewUserController.g.cs
using Microsoft.VisualStudio.Services.Licensing;
using TfsCmdlets.Cmdlets.Identity;
using Microsoft.VisualStudio.Services.Licensing.Client;
using Newtonsoft.Json;
using TfsCmdlets.Util;
namespace TfsCmdlets.Controllers.Identity.User
{
    internal partial class NewUserController: ControllerBase
    {
        // User
        protected bool Has_User { get; set; }
        protected string User { get; set; }
        // DisplayName
        protected bool Has_DisplayName { get; set; }
        protected string DisplayName { get; set; }
        // License
        protected bool Has_License { get; set; }
        protected TfsCmdlets.AccountLicenseType License { get; set; }
        // Project
        protected bool Has_Project { get; set; }
        protected object Project { get; set; }
        // DefaultGroup
        protected bool Has_DefaultGroup { get; set; }
        protected TfsCmdlets.GroupEntitlementType DefaultGroup { get; set; }
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
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Licensing.AccountEntitlement);
        protected override void CacheParameters()
        {
            // User
            Has_User = Parameters.HasParameter("User");
            User = Parameters.Get<string>("User");
            // DisplayName
            Has_DisplayName = Parameters.HasParameter("DisplayName");
            DisplayName = Parameters.Get<string>("DisplayName");
            // License
            Has_License = Parameters.HasParameter("License");
            License = Parameters.Get<TfsCmdlets.AccountLicenseType>("License", AccountLicenseType.Stakeholder);
            // Project
            Has_Project = Parameters.HasParameter("Project");
            Project = Parameters.Get<object>("Project");
            // DefaultGroup
            Has_DefaultGroup = Parameters.HasParameter("DefaultGroup");
            DefaultGroup = Parameters.Get<TfsCmdlets.GroupEntitlementType>("DefaultGroup", GroupEntitlementType.Contributor);
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewUserController(IRestApiService restApi, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApi = restApi;
        }
    }
}