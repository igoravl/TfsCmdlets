using System.Management.Automation;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.WebApi.Contracts.DelegatedAuthorization;

namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    /// <summary>
    /// Renames a personal access token (PAT).
    /// </summary>
    /// <remarks>
    /// This command updates the display name of a token without changing its scope or expiration date.
    /// To regenerate a token, use Update-TfsPersonalAccessToken.
    /// The token must be valid (not revoked) to be updated.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, HostedOnly = true,
        SupportsShouldProcess = true,
        OutputType = typeof(PatToken))]
    partial class RenamePersonalAccessToken
    {
        /// <summary>
        /// Specifies the personal access token to update. Accepts a Guid (authorizationId)
        /// or a PatToken object.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("Name", "DisplayName", "Pat")]
        public object PersonalAccessToken { get; set; }
    }

    [CmdletController(typeof(PatToken), Client = typeof(ITokensHttpClient))]
    partial class RenamePersonalAccessTokenController
    {
        protected override IEnumerable Run()
        {
            foreach (var item in Items)
            {
                var authorizationId = item.AuthorizationId;

                if (!PowerShell.ShouldProcess($"[Token: {item.DisplayName}] ({item.AuthorizationId})",
                    "Update personal access token"))
                {
                    yield break;
                }

                var request = new PatTokenUpdateRequest
                {
                    AuthorizationId = authorizationId,
                    DisplayName = NewName
                };

                var result = Client.UpdatePatAsync(request)
                    .GetResult("Error updating personal access token");

                if (result.PatTokenError != SessionTokenError.None)
                {
                    throw new Exception($"Error updating personal access token '{item.DisplayName}': {result.PatTokenError}");
                }

                yield return result.PatToken;
            }
        }
    }
}
