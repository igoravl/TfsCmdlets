using Microsoft.VisualStudio.Services.Licensing;
using TfsCmdlets.Cmdlets.Identity;
using Microsoft.VisualStudio.Services.Licensing.Client;

namespace TfsCmdlets.Controllers.Identity
{
    [CmdletController(typeof(AccountEntitlement))]
    partial class GetUserController
    {
        public override IEnumerable<AccountEntitlement> Invoke()
        {
            var user = Parameters.Get<object>(nameof(GetUser.User));
            var current = Parameters.Get<bool>(nameof(GetUser.Current));

            var client = Data.GetClient<AccountLicensingHttpClient>();

            if (current)
            {
                user = Data.GetItem<Models.Identity>(new { Identity = user });

                if (user == null) return null;
            }

            switch (user)
            {
                case AccountEntitlement entitlement:
                    {
                        return new[] { entitlement };
                    }
                case WebApiIdentity i:
                    {
                        return new[] { client.GetAccountEntitlementAsync(i.Id).GetResult("Error getting account entitlement") };
                    }
                case WebApiIdentityRef ir:
                    {
                        return new[] { client.GetAccountEntitlementAsync(ir.Id).GetResult("Error getting account entitlement") };
                    }
                case string s when s.IsGuid():
                    {
                        return new[] { client.GetAccountEntitlementAsync(new Guid(s)).GetResult("Error getting account entitlement") };
                    }
                case Guid g:
                    {
                        return new[] { client.GetAccountEntitlementAsync(g).GetResult("Error getting account entitlement") };
                    }
                case string s:
                    {
                        return client.GetAccountEntitlementsAsync()
                            .GetResult("Error getting account entitlements")
                            .Where(u => u.User.DisplayName.IsLike(s) || u.User.UniqueName.IsLike(s));
                    }
            }

            Logger.LogError(new ArgumentException($"Invalid or non-existent user {user}"));
            return null;
        }
    }
}