using Microsoft.VisualStudio.Services.Identity;

namespace TfsCmdlets.Controllers.Identity
{
    [CmdletController(typeof(Models.Identity))]
    partial class GetIdentityController
    {
        public override IEnumerable<Models.Identity> Invoke()
        {
            var current = Parameters.Get<bool>("Current");
            var queryMembership = Parameters.Get<TfsQueryMembership>("QueryMembership");
            var identity = Parameters.Get<object>("Identity");

            if (current)
            {
                var srv = Data.GetServer();
                if (srv == null) return null;

                identity = srv.CurrentUserUniqueName;
            }

            var client = Data.GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>(ClientScope.Server);
            var qm = queryMembership;

            switch (identity)
            {
                case WebApiIdentity i:
                    {
                        return new[] { new Models.Identity(i) };
                    }
                case WebApiIdentityRef ir:
                    {
                        return new[]{ new Models.Identity(client.ReadIdentityAsync(ir.Id, qm)
                            .GetResult($"Error retrieving information from identity [{identity}]")) };
                    }
                case string s when s.IsGuid():
                    {
                        var g = new Guid(s);

                        return new[]{ new Models.Identity(client.ReadIdentityAsync(g, qm)
                            .GetResult($"Error retrieving information from identity [{identity}]")) };
                    }
                case Guid g:
                    {
                        Logger.Log($"Finding identity with ID [{g}] and QueryMembership={qm}");

                        return new[]{ new Models.Identity(client.ReadIdentityAsync(g, qm)
                            .GetResult($"Error retrieving information from identity [{identity}]")) };
                    }
                case string s:
                    {
                        Logger.Log($"Finding identity with account name [{identity}] and QueryMembership={qm}");

                        return client.ReadIdentitiesAsync(IdentitySearchFilter.AccountName, s, ReadIdentitiesOptions.None, qm)
                            .GetResult($"Error retrieving information from identity [{identity}]")
                            .Select(i => new Models.Identity(i));
                    }

            }

            Logger.LogError(new ArgumentException($"Invalid or non-existent idehtity {identity}"));
            return null;
        }
    }
}