using System.Management.Automation;
using Microsoft.VisualStudio.Services.Licensing;
using TfsCmdlets.Cmdlets.Identity;
using Microsoft.VisualStudio.Services.Licensing.Client;

namespace TfsCmdlets.Cmdlets.Identity.User
{
    /// <summary>
    /// Gets information about one or more Azure DevOps users.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(AccountEntitlement), NoAutoPipeline = true, DefaultParameterSetName = "Get by ID or Name")]
    partial class GetUser
    {
        /// <summary>
        /// Specifies the user or group to be retrieved. Supported values are: 
        /// User/group name, email, or ID
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by ID or Name", ValueFromPipeline = true)]
        [Alias("UserId")]
        [SupportsWildcards]
        public object User { get; set; } = "*";

        /// <summary>
        /// Returns an identity representing the user currently logged in to
        /// the Azure DevOps / TFS instance
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current user")]
        public SwitchParameter Current { get; set; }
    }

    [CmdletController(typeof(AccountEntitlement))]
    partial class GetUserController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<AccountLicensingHttpClient>();

            if (Current)
            {
                var user = Data.GetItem<Models.Identity>(new { Identity = User });

                yield return user;
                yield break;
            }

            foreach (var input in User)
            {
                var user = input switch
                {
                    string s when s.IsGuid() => new Guid(s),
                    _ => input
                };

                switch (user)
                {
                    case AccountEntitlement entitlement:
                        {
                            yield return entitlement;
                            break;
                        }
                    case WebApiIdentity i:
                        {
                            yield return client.GetAccountEntitlementAsync(i.Id).GetResult("Error getting account entitlement");
                            break;
                        }
                    case WebApiIdentityRef ir:
                        {
                            yield return client.GetAccountEntitlementAsync(ir.Id).GetResult("Error getting account entitlement");
                            break;
                        }
                    case Guid g:
                        {
                            yield return client.GetAccountEntitlementAsync(g).GetResult("Error getting account entitlement");
                            break;
                        }
                    case string s:
                        {
                            foreach (var u in client.GetAccountEntitlementsAsync()
                                .GetResult("Error getting account entitlements")
                                .Where(u => u.User.DisplayName.IsLike(s) || u.User.UniqueName.IsLike(s)))
                            {
                                yield return u;
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent user {user}"));
                            break;
                        }
                }
            }
        }
    }
}