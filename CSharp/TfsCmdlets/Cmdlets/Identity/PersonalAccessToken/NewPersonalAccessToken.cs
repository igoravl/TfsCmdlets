using System.Management.Automation;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;

namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    /// <summary>
    /// Creates a new personal access token (PAT) for the current user.
    /// </summary>
    /// <remarks>
    /// The token string is only available at creation time and cannot be retrieved later.
    /// Make sure to save the token value returned by this command.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, HostedOnly = true,
        SupportsShouldProcess = true, ReturnsValue = true,
        OutputType = typeof(PatToken))]
    partial class NewPersonalAccessToken
    {
        /// <summary>
        /// Specifies the display name of the new personal access token.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("Name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Specifies the scope(s) of the new personal access token. 
        /// When omitted, defaults to full access ("app_token").
        /// </summary>
        [Parameter(Position = 1)]
        public string[] Scope { get; set; }

        /// <summary>
        /// Specifies the expiration date of the new personal access token.
        /// When omitted, defaults to 30 days from now.
        /// </summary>
        [Parameter(Position = 2)]
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// When set, the token will be valid for all of the user's accessible organizations.
        /// </summary>
        [Parameter]
        public SwitchParameter AllOrganizations { get; set; }
    }

    [CmdletController(typeof(PatToken), Client = typeof(ITokensHttpClient))]
    partial class NewPersonalAccessTokenController
    {
        protected override IEnumerable Run()
        {
            if (!PowerShell.ShouldProcess($"[Organization: {Data.GetCollection().DisplayName}]",
                $"Create personal access token '{DisplayName}'"))
            {
                yield break;
            }

            var request = new PatTokenCreateRequest
            {
                DisplayName = DisplayName,
                Scope = Has_Scope ? string.Join(" ", Scope) : "app_token",
                ValidTo = Has_ValidTo ? ValidTo : DateTime.UtcNow.AddDays(30),
                AllOrgs = AllOrganizations
            };

            var result = Client.CreatePatAsync(request)
                .GetResult("Error creating personal access token");

            if (result.PatTokenError != SessionTokenError.None)
            {
                throw new Exception($"Error creating personal access token '{DisplayName}': {result.PatTokenError}");
            }

            Logger.LogWarn("Save the token value now. It cannot be retrieved later.");

            yield return result.PatToken;
        }
    }
}
