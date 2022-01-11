using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Identity.Client;

namespace TfsCmdlets.Controllers.Identity
{
    [CmdletController(typeof(Models.Identity))]
    partial class GetIdentityController
    {
        protected override IEnumerable Run()
        {
            var current = Parameters.Get<bool>("Current");
            var queryMembership = Parameters.Get<TfsQueryMembership>("QueryMembership");
            var identity = Parameters.Get<object>("Identity");
            ICollection identities;

            if (current)
            {
                var srv = Data.GetServer();
                if (srv == null) yield break;

                identity = srv.CurrentUserUniqueName;
            }

            var client = Data.GetClient<IdentityHttpClient>(ClientScope.Server);
            var qm = queryMembership;

            identities = (identity is ICollection list) ?
                list :
                new object[] { identity };

            foreach (var ident in identities)
            {
                switch (ident)
                {
                    case Models.Identity i:
                        {
                            yield return i;
                            break;
                        }
                    case WebApiIdentity i:
                        {
                            yield return new Models.Identity(i);
                            break;
                        }
                    case WebApiIdentityRef ir:
                        {
                            yield return new Models.Identity(client.ReadIdentityAsync(ir.Id, qm)
                                .GetResult($"Error retrieving information from identity [{identity}]"));
                            break;
                        }
                    case string s when s.IsGuid():
                        {
                            var g = new Guid(s);
                            Logger.Log($"Finding identity with ID [{g}] and QueryMembership={qm}");

                            yield return new Models.Identity(client.ReadIdentityAsync(g, qm)
                                .GetResult($"Error retrieving information from identity [{identity}]"));
                            break;
                        }
                    case Guid g:
                        {
                            Logger.Log($"Finding identity with ID [{g}] and QueryMembership={qm}");

                            yield return new Models.Identity(client.ReadIdentityAsync(g, qm)
                                .GetResult($"Error retrieving information from identity [{identity}]"));
                            break;
                        }
                    case string s:
                        {
                            Logger.Log($"Finding identity with account name [{identity}] and QueryMembership={qm}");

                            foreach (var id in client.ReadIdentitiesAsync(IdentitySearchFilter.AccountName, s, ReadIdentitiesOptions.None, qm)
                                .GetResult($"Error retrieving information from identity [{identity}]")
                                .Select(i => new Models.Identity(i)))
                            {
                                yield return id;
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent identity '{ident}'"));
                            break;
                        }
                }
            }
        }
    }
}