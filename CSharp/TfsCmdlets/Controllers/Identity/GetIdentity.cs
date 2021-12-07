using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Extensions;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;

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
                if (srv == null) yield break;

                identity = srv.AuthorizedIdentity.UniqueName;
            }

            var client = Data.GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>(ClientScope.Server);
            var qm = queryMembership;

            while (true) switch (identity)
                {
                    case WebApiIdentity i:
                        {
                            yield return new Models.Identity(i);
                            yield break;
                        }
                    case string s when s.IsGuid():
                        {
                            identity = new Guid(s);
                            continue;
                        }
                    case Guid g:
                        {
                            Logger.Log($"Finding identity with ID [{g}] and QueryMembership={qm}");

                            var result = client.ReadIdentityAsync(g, qm)
                                .GetResult($"Error retrieving information from identity [{identity}]");

                            yield return new Models.Identity(result);
                            yield break;
                        }
                    case string s:
                        {
                            Logger.Log($"Finding identity with account name [{identity}] and QueryMembership={qm}");

                            var result = client.ReadIdentitiesAsync(IdentitySearchFilter.AccountName, s, ReadIdentitiesOptions.None, qm)
                                .GetResult($"Error retrieving information from identity [{identity}]");

                            foreach (var i in result) yield return new Models.Identity(i);
                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent idehtity {identity}");
                        }
                }
        }
    }
}