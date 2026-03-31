using System.Management.Automation;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;

namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    /// <summary>
    /// Regenerates a personal access token (PAT), producing a new token string.
    /// </summary>
    /// <remarks>
    /// This command revokes the existing token and creates a new one with the same 
    /// (or overridden) properties. The new token string is only available at creation time.
    /// The old token will stop working immediately after this command completes.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, HostedOnly = true,
        SupportsShouldProcess = true,
        OutputType = typeof(PatToken))]
    partial class UpdatePersonalAccessToken
    {
        /// <summary>
        /// Specifies the personal access token to regenerate. Accepts a Guid (authorizationId)
        /// or a PatToken object.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("Name", "DisplayName", "Pat")]
        public object PersonalAccessToken { get; set; }

        /// <summary>
        /// Specifies a new scope for the regenerated token. When omitted, 
        /// the scope of the original token is preserved.
        /// </summary>
        [Parameter]
        public string[] Scope { get; set; }

        /// <summary>
        /// Specifies a new expiration date for the regenerated token. When omitted, 
        /// the original expiration date is preserved.
        /// </summary>
        [Parameter]
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// When set, the regenerated token will be valid for all of the user's accessible organizations.
        /// </summary>
        [Parameter]
        public SwitchParameter AllOrganizations { get; set; }
    }

    [CmdletController(typeof(PatToken), Client = typeof(ITokensHttpClient))]
    partial class UpdatePersonalAccessTokenController
    {
        protected override IEnumerable Run()
        {
            foreach (var item in Items)
            {
                var authorizationId = item.AuthorizationId;

                if (!PowerShell.ShouldProcess(
                    $"[Token: {item.DisplayName}] ({item.AuthorizationId})",
                    "Regenerate personal access token"))
                {
                    yield break;
                }

                // Revoke old token
                Client.RevokeAsync(authorizationId)
                    .Wait("Error revoking personal access token");

                // Create new token with same or overridden properties
                var request = new PatTokenCreateRequest
                {
                    DisplayName = item.DisplayName,
                    Scope = Has_Scope ? string.Join(" ", Scope) : item.Scope,
                    ValidTo = Has_ValidTo ? ValidTo : item.ValidTo,
                    AllOrgs = Has_AllOrganizations ? AllOrganizations : item.TargetAccounts == null
                };

                var result = Client.CreatePatAsync(request)
                    .GetResult("Error creating new personal access token");

                if (result.PatTokenError != SessionTokenError.None)
                {
                    throw new Exception($"Error regenerating personal access token '{item.DisplayName}': {result.PatTokenError}. " +
                        "Warning: The original token has been revoked.");
                }

                Logger.LogWarn("Save the new token value now. It cannot be retrieved later.");

                yield return result.PatToken;
            }
        }
    }
}
