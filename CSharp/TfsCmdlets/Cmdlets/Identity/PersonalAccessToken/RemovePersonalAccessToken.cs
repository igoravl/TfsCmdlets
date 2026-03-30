using System.Management.Automation;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.TokenAdmin.Client;

namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    /// <summary>
    /// Revokes (removes) a personal access token (PAT). 
    /// Administrators can revoke another user's tokens by specifying the -User parameter.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, HostedOnly = true,
        SupportsShouldProcess = true,
        DefaultParameterSetName = "Remove own token")]
    partial class RemovePersonalAccessToken
    {
    {
        /// <summary>
        /// Specifies the personal access token to revoke. Accepts a Guid (authorizationId)
        /// or a PatToken object.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Remove own token")]
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Remove for user")]
        public object PersonalAccessToken { get; set; }

        /// <summary>
        /// Specifies a user whose token should be revoked. Requires administrator privileges.
        /// The value should be a SubjectDescriptor (e.g. 'aad.xxx' or 'msa.xxx').
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Remove for user")]
        public string User { get; set; }

        /// <summary>
        /// Suppresses the confirmation prompt for the token revocation.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(PatToken), Client = typeof(ITokensHttpClient))]
    partial class RemovePersonalAccessTokenController
    {
        [Import]
        private ITokenAdminHttpClient TokenAdminClient { get; set; }

        protected override IEnumerable Run()
        {
            var authorizationId = PersonalAccessToken switch
            {
                Guid guid => guid,
                PatToken token => token.AuthorizationId,
                string s when Guid.TryParse(s, out var guid) => guid,
                _ => throw new ArgumentException($"Invalid personal access token type: {PersonalAccessToken?.GetType().Name ?? "null"}. Expected Guid or PatToken.")
            };

            var tokenDescription = PersonalAccessToken is PatToken t 
                ? $"[Token: {t.DisplayName}] ({t.AuthorizationId})" 
                : $"[Token: {authorizationId}]";

            if (Has_User)
            {
                // Admin revocation
                if (!PowerShell.ShouldProcess(
                    $"{tokenDescription} for user '{User}'",
                    "Revoke personal access token (admin)"))
                {
                    yield break;
                }

                if (!Force && !PowerShell.ShouldContinue(
                    $"Are you sure you want to revoke token {tokenDescription} for user '{User}'? This operation is irreversible."))
                {
                    yield break;
                }

                var revocation = new TokenAdminRevocation
                {
                    AuthorizationId = authorizationId
                };

                TokenAdminClient.RevokeAuthorizationsAsync(
                    new[] { revocation })
                    .Wait("Error revoking personal access token for user");
            }
            else
            {
                // Self revocation
                if (!PowerShell.ShouldProcess(
                    tokenDescription,
                    "Revoke personal access token"))
                {
                    yield break;
                }

                if (!Force && !PowerShell.ShouldContinue(
                    $"Are you sure you want to revoke token {tokenDescription}? This operation is irreversible."))
                {
                    yield break;
                }

                Client.RevokeAsync(authorizationId)
                    .Wait("Error revoking personal access token");
            }
        }
    }
}
