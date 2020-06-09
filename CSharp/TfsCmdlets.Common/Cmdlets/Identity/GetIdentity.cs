using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Extensions;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;

namespace TfsCmdlets.Cmdlets.Identity
{
    /// <summary>
    /// Gets one or more identities that represents either users or groups in Azure DevOps.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsIdentity")]
    public partial class GetIdentity : BaseCmdlet
    {
        /// <summary>
        /// Specifies the user or group to be retrieved. Supported values are: 
        /// User/group name, email, or ID
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Get Identity")]
        public object Identity { get; set; }

        /// <summary>
        /// Specifies that group membership information should be included 
        /// in the identity (applies only to groups)
        /// </summary>
        [Parameter(ParameterSetName = "Get Identity")]
        public SwitchParameter QueryMembership { get; set; }

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

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if (Current)
            {
                var srv = GetServer();

                if (srv == null) return;

                WriteObject(srv.AuthorizedIdentity);

                return;
            }

            var client = GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>(ClientScope.Server);
            var qm = this.QueryMembership? TfsQueryMembership.Direct: TfsQueryMembership.None;

            while (true) switch(Identity)
            {
                case PSObject pso:
                {
                    Identity = pso.BaseObject;
                    continue;
                }
                case object o when o.GetType().IsAssignableFrom(IdentityType):
                {
                    WriteObject(o);
                    return;
                }
                case string s when s.IsGuid():
                {
                    Identity = new Guid(s);
                    continue;
                }
                case Guid g:
                {
                    this.Log($"Finding identity with ID [{Identity}] and QueryMembership={qm}");

                    var result = client.ReadIdentityAsync(g)
                        .GetResult($"Error retrieving information from identity [{Identity}]");

                    WriteObject(result);
                    return;
                }
                case string s:
                {
                    this.Log($"Finding identity with account name [{Identity}] and QueryMembership={qm}");
                    
                    var result = client.ReadIdentitiesAsync(IdentitySearchFilter.AccountName, s, ReadIdentitiesOptions.None, qm)
                        .GetResult($"Error retrieving information from identity [{Identity}]");

                    WriteObject(result, true);
                    return;
                }
            }
        }
    }
}