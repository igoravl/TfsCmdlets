using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Identity.Client;
using Microsoft.VisualStudio.Services.Licensing;

namespace TfsCmdlets.Cmdlets.Identity
{
    /// <summary>
    /// Gets one or more identities that represents either users or groups in Azure DevOps. 
    /// This cmdlets resolves legacy identity information for use with older APIs such as the Security APIs
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, NoAutoPipeline = true, OutputType = typeof(WebApiIdentity))]
    partial class GetIdentity 
    {
        /// <summary>
        /// Specifies the user or group to be retrieved. Supported values are: 
        /// User/group name, email, or ID
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Get Identity", ValueFromPipelineByPropertyName = true)]
        [Alias("User", "Id", "Group")]
        public object Identity { get; set; }

        /// <summary>
        /// Specifies how group membership information should be processed  
        /// when the returned identity is a group. "Direct" fetches direct members (both users 
        /// and groups) of the group. "Expanded" expands contained groups recursively and returns 
        /// their contained users. "None" is the fastest option as it fetches no membership 
        /// information. When omitted, defaults to Direct.
        /// </summary>
        [Parameter(ParameterSetName = "Get Identity")]
        public WebApiQueryMembership QueryMembership { get; set; } = WebApiQueryMembership.Direct;

        /// <summary>
        /// Returns an identity representing the user currently logged in to
        /// the Azure DevOps / TFS instance
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current user")]
        public SwitchParameter Current { get; set; }
    }

    [CmdletController(typeof(Models.Identity), Client=typeof(IIdentityHttpClient))]
    partial class GetIdentityController
    {
        protected override IEnumerable Run()
        {
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
                            yield return new Models.Identity(Client.ReadIdentityAsync(ir.Id, QueryMembership)
                                .GetResult($"Error retrieving information from identity [{identity}]"));
                            break;
                        }
                    case Guid g:
                        {
                            Logger.Log($"Finding identity with ID [{g}] and QueryMembership={QueryMembership}");

                            yield return new Models.Identity(Client.ReadIdentityAsync(g, QueryMembership)
                                .GetResult($"Error retrieving information from identity [{identity}]"));
                            break;
                        }
                    case string s:
                        {
                            Logger.Log($"Finding identity with account name [{identity}] and QueryMembership={QueryMembership}");

                            foreach (var id in Client.ReadIdentitiesAsync(IdentitySearchFilter.AccountName, s, ReadIdentitiesOptions.None, QueryMembership)
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