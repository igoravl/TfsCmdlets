using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Identity.Client;
using Microsoft.VisualStudio.Services.Licensing;

namespace TfsCmdlets.Controllers.Identity
{
    [CmdletController(typeof(Models.Identity))]
    partial class GetIdentityController
    {
        protected override IEnumerable Run()
        {
            var client = Data.GetClient<IdentityHttpClient>(); // ClientScope.Server

            foreach (var input in Identity)
            {
                var identity = input switch
                {
                    null when Current => Data.GetServer()?.CurrentUserUniqueName,
                    AccountEntitlement ae => ae.UserId,
                    string s when s.IsGuid() => new Guid(s),
                    _ => input
                };

                switch (identity)
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
                            yield return new Models.Identity(client.ReadIdentityAsync(ir.Id, QueryMembership)
                                .GetResult($"Error retrieving information from identity [{identity}]"));
                            break;
                        }
                    case Guid g:
                        {
                            Logger.Log($"Finding identity with ID [{g}] and QueryMembership={QueryMembership}");

                            yield return new Models.Identity(client.ReadIdentityAsync(g, QueryMembership)
                                .GetResult($"Error retrieving information from identity [{identity}]"));
                            break;
                        }
                    case string s:
                        {
                            Logger.Log($"Finding identity with account name [{identity}] and QueryMembership={QueryMembership}");

                            foreach (var id in client.ReadIdentitiesAsync(IdentitySearchFilter.AccountName, s, ReadIdentitiesOptions.None, QueryMembership)
                                .GetResult($"Error retrieving information from identity [{identity}]")
                                .Select(i => new Models.Identity(i)))
                            {
                                yield return id;
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent identity '{identity}'"));
                            break;
                        }
                }
            }
        }
    }
}