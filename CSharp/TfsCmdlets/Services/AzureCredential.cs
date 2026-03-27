using Azure.Core;
using Azure.Identity;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.OAuth;

namespace TfsCmdlets.Services
{
    /// <summary>
    /// Helper class that wraps Azure.Identity's DefaultAzureCredential to obtain and 
    /// automatically renew Azure DevOps access tokens.
    /// </summary>
    /// <remarks>
    /// Tokens issued by Azure AD for Azure DevOps have a short lifetime (~1 hour).
    /// This class transparently refreshes the token before it expires by checking expiry 
    /// when <see cref="GetValidToken"/> is called. The underlying <see cref="DefaultAzureCredential"/>
    /// chains multiple credential sources (Environment, Managed Identity, Azure CLI,
    /// Visual Studio, etc.) so the caller only needs to be authenticated in one of them.
    /// </remarks>
    public sealed class AzureCredential
    {
        // Azure DevOps resource ID used as the token audience
        private const string AzureDevOpsScope = "499b84ac-1321-427f-aa17-267ca6975798/.default";

        // Refresh the token when it is within this margin of expiry
        private static readonly TimeSpan RefreshMargin = TimeSpan.FromMinutes(5);

        private readonly TokenCredential _tokenCredential;
        private AccessToken _cachedToken;
        private readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance using the default Azure credential chain.
        /// </summary>
        public AzureCredential()
            : this(new DefaultAzureCredential(
                new DefaultAzureCredentialOptions
                {
                    // Disable interactive browser auth to avoid hangs in unattended (CI/CD) scenarios.
                    ExcludeInteractiveBrowserCredential = true
                }))
        {
        }

        /// <summary>
        /// Initializes a new instance using the specified Azure <see cref="TokenCredential"/>.
        /// </summary>
        public AzureCredential(TokenCredential tokenCredential)
        {
            _tokenCredential = tokenCredential ?? throw new ArgumentNullException(nameof(tokenCredential));

            // Eagerly acquire the first token to fail fast on auth errors
            _cachedToken = _tokenCredential.GetToken(
                new TokenRequestContext(new[] { AzureDevOpsScope }),
                default);
        }

        /// <summary>
        /// Returns true if the cached token is expired or near expiry.
        /// </summary>
        public bool IsTokenExpired
        {
            get
            {
                lock (_lock)
                {
                    return _cachedToken.ExpiresOn <= DateTimeOffset.UtcNow.Add(RefreshMargin);
                }
            }
        }

        /// <summary>
        /// Gets a current, valid access token, refreshing from Azure if the cached token 
        /// is expired or near expiry.
        /// </summary>
        public string GetValidToken()
        {
            lock (_lock)
            {
                if (_cachedToken.ExpiresOn <= DateTimeOffset.UtcNow.Add(RefreshMargin))
                {
                    _cachedToken = _tokenCredential.GetToken(
                        new TokenRequestContext(new[] { AzureDevOpsScope }),
                        default);
                }

                return _cachedToken.Token;
            }
        }

        /// <summary>
        /// Creates a new <see cref="VssCredentials"/> instance using a valid (potentially refreshed) token.
        /// Each call returns a new instance with a fresh token.
        /// </summary>
        public VssCredentials CreateVssCredentials()
        {
            return new VssCredentials(new VssOAuthAccessTokenCredential(GetValidToken()));
        }
    }
}
