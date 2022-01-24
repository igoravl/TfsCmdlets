using Microsoft.VisualStudio.Services.Licensing;
using TfsCmdlets.Cmdlets.Identity;
using Microsoft.VisualStudio.Services.Licensing.Client;

namespace TfsCmdlets.Controllers.Identity
{
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