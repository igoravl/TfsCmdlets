using System.Management.Automation;
using Microsoft.VisualStudio.Services.Licensing;
using Microsoft.VisualStudio.Services.Licensing.Client;
using IAccountLicensingHttpClient = TfsCmdlets.HttpClients.IAccountLicensingHttpClient;

namespace TfsCmdlets.Cmdlets.Identity.User
{
    /// <summary>
    /// Deletes one or more users from the organization.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(AccountEntitlement))]
    partial class RemoveUser
    {
        /// <summary>
        /// Specifies the user to be removed from the organization.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object User { get; set; }
    }
}

// Controller

namespace TfsCmdlets.Controllers.Identity.User
{
    [CmdletController(typeof(AccountEntitlement), Client=typeof(IAccountLicensingHttpClient))]
    partial class RemoveUserController
    {
        protected override IEnumerable Run()
        {
            foreach (var user in Items)
            {
                if (!PowerShell.ShouldProcess(Collection, $"Delete user '{user.User.UniqueName}' ('{user.User.DisplayName}')")) continue;

                try
                {
                    Client.DeleteEntitlementAsync(Guid.Parse(user.User.Id)).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            return null;
        }
    }
}