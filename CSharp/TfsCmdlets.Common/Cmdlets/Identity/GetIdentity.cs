using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;

namespace TfsCmdlets.Cmdlets.Identity
{
    /// <summary>
    /// Gets one or more identities that represents either users or groups in Azure DevOps.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsIdentity")]
    [OutputType(typeof(WebApiIdentity))]
    public partial class GetIdentity : BaseCmdlet
    {
        /// <summary>
        /// Specifies the user or group to be retrieved. Supported values are: 
        /// User/group name, email, or ID
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Get Identity")]
        public object Identity { get; set; }

        /// <summary>
        /// Specifies how group membership information should be processed  
        /// when the returned identity is a group. "Direct" fetches direct members (both users 
        /// and groups) of the group. "Expanded" expands contained groups recursively and returns 
        /// their contained users. "None" is the fastest option as it fetches no membership 
        /// information. When omitted, defaults to Direct.
        /// </summary>
        [Parameter(ParameterSetName = "Get Identity")]
        public TfsQueryMembership QueryMembership { get; set; } = TfsQueryMembership.Direct;

        /// <summary>
        /// Returns an identity representing the user currently logged in to
        /// the Azure DevOps / TFS instance
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current user")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Server { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteItems<Models.Identity>();
        }
    }

    [Exports(typeof(Models.Identity))]
    internal partial class IdentityDataService : BaseDataService<Models.Identity>
    {
        protected override System.Collections.Generic.IEnumerable<Models.Identity> DoGetItems()
        {
            var current = GetParameter<bool>("Current");
            var queryMembership = GetParameter<TfsQueryMembership>("QueryMembership");
            var identity = GetParameter<object>("Identity");

            if (current)
            {
                var srv = GetServer();
                if (srv == null) yield break;

                identity = srv.AuthorizedIdentity.UniqueName;
            }

            var client = GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>(ClientScope.Server);
            var qm = queryMembership;

            while (true) switch(identity)
            {
                case PSObject pso:
                {
                    identity = pso.BaseObject;
                    continue;
                }
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

                    foreach(var i in result) yield return new Models.Identity(i);
                    yield break;
                }
                default: {
                    throw new ArgumentException($"Invalid or non-existent idehtity {identity}");
                }
            }
        }
    }
}