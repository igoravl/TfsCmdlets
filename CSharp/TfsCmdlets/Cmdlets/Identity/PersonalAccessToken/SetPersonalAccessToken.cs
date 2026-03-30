using System.Management.Automation;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.WebApi.Contracts.DelegatedAuthorization;

namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    /// <summary>
    /// Edits the properties of an existing personal access token (PAT).
    /// </summary>
    /// <remarks>
    /// This command updates the metadata of a token (display name, scope, expiration date)
    /// without regenerating its value. To regenerate a token, use Update-TfsPersonalAccessToken.
    /// The token must be valid (not revoked) to be updated.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, HostedOnly = true,
        SupportsShouldProcess = true,
        OutputType = typeof(PatToken))]
    partial class SetPersonalAccessToken
    {
        /// <summary>
        /// Specifies the personal access token to update. Accepts a Guid (authorizationId)
        /// or a PatToken object.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object PersonalAccessToken { get; set; }

        /// <summary>
        /// Specifies the new display name for the token.
        /// </summary>
        [Parameter]
        [Alias("Name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Specifies the new scope for the token.
        /// </summary>
        [Parameter]
        public string Scope { get; set; }

        /// <summary>
        /// Specifies the new expiration date for the token.
        /// </summary>
        [Parameter]
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// When set, the token will be valid for all of the user's accessible organizations.
        /// </summary>
        [Parameter]
        public SwitchParameter AllOrganizations { get; set; }
    }

    [CmdletController(typeof(PatToken), Client = typeof(ITokensHttpClient))]
    partial class SetPersonalAccessTokenController
    {
        protected override IEnumerable Run()
        {
            var authorizationId = PersonalAccessToken switch
            {
                Guid guid => guid,
                PatToken token => token.AuthorizationId,
                string s when Guid.TryParse(s, out var guid) => guid,
                _ => throw new ArgumentException($"Invalid personal access token type: {PersonalAccessToken?.GetType().Name ?? "null"}. Expected Guid or PatToken.")
            };

            // Get current token to display in ShouldProcess
            var currentResult = Client.GetPatAsync(authorizationId)
                .GetResult("Error getting personal access token");

            if (currentResult.PatTokenError != SessionTokenError.None)
            {
                throw new Exception($"Error getting personal access token: {currentResult.PatTokenError}");
            }

            var currentToken = currentResult.PatToken;

            if (!PowerShell.ShouldProcess(
                $"[Token: {currentToken.DisplayName}] ({currentToken.AuthorizationId})",
                "Update personal access token"))
            {
                yield break;
            }

            var request = new PatTokenUpdateRequest
            {
                AuthorizationId = authorizationId
            };

            if (Has_DisplayName)
                request.DisplayName = DisplayName;

            if (Has_Scope)
                request.Scope = Scope;

            if (Has_ValidTo)
                request.ValidTo = ValidTo;

            if (Has_AllOrganizations)
                request.AllOrgs = AllOrganizations;

            var result = Client.UpdatePatAsync(request)
                .GetResult("Error updating personal access token");

            if (result.PatTokenError != SessionTokenError.None)
            {
                throw new Exception($"Error updating personal access token '{currentToken.DisplayName}': {result.PatTokenError}");
            }

            yield return result.PatToken;
        }
    }
}
