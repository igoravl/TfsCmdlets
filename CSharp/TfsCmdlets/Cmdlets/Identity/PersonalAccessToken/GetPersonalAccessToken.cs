using System.Management.Automation;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.TokenAdmin.Client;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    /// <summary>
    /// Gets one or more personal access tokens (PATs) for the current user, 
    /// or lists PATs of another user when running as an administrator.
    /// </summary>
    /// <remarks>
    /// The PAT Lifecycle Management API only allows users to manage their own tokens.
    /// Administrators can list tokens of other users by specifying the -User parameter,
    /// which uses the Token Admin API.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, HostedOnly = true,
        DefaultParameterSetName = "Get by name",
        OutputType = typeof(PatToken))]
    partial class GetPersonalAccessToken
    {
        /// <summary>
        /// Specifies the personal access token to retrieve. Accepts a token display name 
        /// (wildcards supported), a Guid (authorizationId), or a PatToken object.
        /// When omitted, returns all tokens matching the filter criteria.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by name")]
        [Parameter(Position = 0, ParameterSetName = "Get for user")]
        [Alias("Name", "DisplayName")]
        [SupportsWildcards]
        public object PersonalAccessToken { get; set; } = "*";

        /// <summary>
        /// Specifies the personal access token to retrieve by its authorization ID.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get by id")]
        public Guid AuthorizationId { get; set; }

        /// <summary>
        /// Filters tokens by status. Valid values are Active, Revoked, Expired, and All.
        /// Defaults to Active.
        /// </summary>
        [Parameter(ParameterSetName = "Get by name")]
        [Parameter(ParameterSetName = "Get for user")]
        public DisplayFilterOptions DisplayFilter { get; set; } = DisplayFilterOptions.Active;

        /// <summary>
        /// Specifies the field to sort results by. Valid values are DisplayName, DisplayDate, and Status.
        /// </summary>
        [Parameter(ParameterSetName = "Get by name")]
        [Parameter(ParameterSetName = "Get for user")]
        public SortByOptions SortBy { get; set; }

        /// <summary>
        /// When set, sorts results in ascending order.
        /// </summary>
        [Parameter(ParameterSetName = "Get by name")]
        [Parameter(ParameterSetName = "Get for user")]
        public SwitchParameter IsSortAscending { get; set; }

        /// <summary>
        /// Specifies a user whose tokens should be listed. Requires administrator privileges.
        /// Accepts Identity objects or subject descriptors. When specified, uses the Token Admin API.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get for user")]
        public object User { get; set; }
    }

    [CmdletController(typeof(PatToken), Client = typeof(ITokensHttpClient))]
    partial class GetPersonalAccessTokenController
    {
        [Import]
        private ITokenAdminHttpClient TokenAdminClient { get; set; }

        protected override IEnumerable Run()
        {
            // Admin scenario: list tokens for another user
            if (ParameterSetName == "Get for user" && Has_User)
            {
                foreach (var token in GetTokensForUser())
                {
                    yield return token;
                }
                yield break;
            }

            // Get by authorization ID
            if (ParameterSetName == "Get by id")
            {
                var result = Client.GetPatAsync(AuthorizationId)
                    .GetResult("Error getting personal access token");

                if (result.PatTokenError != SessionTokenError.None)
                {
                    Logger.LogError($"Error getting personal access token: {result.PatTokenError}");
                    yield break;
                }

                yield return result.PatToken;
                yield break;
            }

            // Resolve input
            foreach (var item in PersonalAccessToken)
            {
                switch (item)
                {
                    case Guid guid:
                    {
                        var result = Client.GetPatAsync(guid)
                            .GetResult("Error getting personal access token");

                        if (result.PatTokenError != SessionTokenError.None)
                        {
                            Logger.LogError($"Error getting personal access token: {result.PatTokenError}");
                            yield break;
                        }

                        yield return result.PatToken;
                        break;
                    }
                    case PatToken token:
                    {
                        yield return token;
                        break;
                    }
                    case string s when Guid.TryParse(s, out var guid):
                    {
                        var result = Client.GetPatAsync(guid)
                            .GetResult("Error getting personal access token");

                        if (result.PatTokenError != SessionTokenError.None)
                        {
                            Logger.LogError($"Error getting personal access token: {result.PatTokenError}");
                            yield break;
                        }

                        yield return result.PatToken;
                        break;
                    }
                    case string pattern:
                    {
                        foreach (var token in ListPats(pattern))
                        {
                            yield return token;
                        }
                        break;
                    }
                    default:
                    {
                        Logger.LogError($"Invalid personal access token type: {item?.GetType().Name ?? "null"}");
                        yield break;
                    }
                }
            }
        }

        private IEnumerable<PatToken> ListPats(string pattern)
        {
            var wildcard = new WildcardPattern(pattern, WildcardOptions.IgnoreCase);
            PagedPatTokens result = null;

            do
            {
                result = Client.ListPatsAsync(
                        displayFilterOption: DisplayFilter,
                        sortByOption: SortBy,
                        isSortAscending: IsSortAscending,
                        continuationToken: result?.ContinuationToken)
                    .GetResult("Error listing personal access tokens");

                foreach (var token in result.PatTokens)
                {
                    if (wildcard.IsMatch(token.DisplayName))
                    {
                        yield return token;
                    }
                }
            } while (!string.IsNullOrEmpty(result?.ContinuationToken));
        }

        private IEnumerable<object> GetTokensForUser()
        {
            var subjectDescriptor = User switch
            {
                SubjectDescriptor sd => sd,
                Models.Identity id => id.SubjectDescriptor,
                string s when s.Contains('.') => new SubjectDescriptor(s.Substring(0, s.IndexOf('.')), s.Substring(s.IndexOf('.') + 1)),
                _ => throw new ArgumentException($"Invalid user type: {User?.GetType().Name ?? "null"}. Expected Identity, SubjectDescriptor, or a descriptor string (e.g. 'aad.xxx').")
            };

            var pattern = PersonalAccessToken is IEnumerable e && e.Cast<object>().FirstOrDefault() is string s2 ? s2 : "*";
            var wildcard = new WildcardPattern(pattern, WildcardOptions.IgnoreCase);

            TokenAdminPagedSessionTokens result = null;

            do
            {
                result = TokenAdminClient.ListPersonalAccessTokensAsync(
                        subjectDescriptor,
                        continuationToken: result?.ContinuationToken?.ToString())
                    .GetResult("Error listing personal access tokens for user");

                foreach (var token in result.SessionTokens)
                {
                    if (wildcard.IsMatch(token.DisplayName))
                    {
                        yield return token;
                    }
                }
            } while (result?.ContinuationToken != null);
        }
    }
}
