using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Desktop;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Management.Automation;
using System;
using System.Linq;
using System.Composition;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IInteractiveAuthentication)), Shared]
    public class InteractiveAuthenticationImpl : IInteractiveAuthentication
    {
        private const string CLIENT_ID = "9f44d9a2-86ef-4794-b2b2-f9038a2628e0";
        private const string SCOPE_ID = "499b84ac-1321-427f-aa17-267ca6975798/user_impersonation";

        /// <summary>
        /// Determines if we're running in PowerShell Core (vs Windows PowerShell)
        /// </summary>
        /// <returns>True if running in PowerShell Core, false otherwise</returns>
        private static bool IsPowerShellCore()
        {
            try
            {
                // Try to access PowerShell version information
                using (var ps = PowerShell.Create())
                {
                    ps.AddScript("$PSVersionTable.PSEdition");
                    var results = ps.Invoke();
                    
                    if (results.Count > 0 && results[0] != null)
                    {
                        return string.Equals(results[0].ToString(), "Core", StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
            catch
            {
                // If we can't determine, assume we're in a context that might need browser fallback
                return true;
            }
            
            return false;
        }

        public string GetToken(Uri uri)
        {
            var authResult = SignInUserAndGetTokenUsingMSAL(new[] { SCOPE_ID }).GetAwaiter().GetResult();
            var authHeader = authResult.AccessToken;

            return authHeader;
        }

        /// <summary>
        /// Sign-in user using MSAL and obtain an access token for Azure DevOps
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns>AuthenticationResult</returns>
        private static async Task<AuthenticationResult> SignInUserAndGetTokenUsingMSAL(string[] scopes)
        {
            var application = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(new PublicClientApplicationOptions
                {
                    AadAuthorityAudience = AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount,
                    ClientId = CLIENT_ID,
                    ClientName = "TfsCmdlets.InteractiveAuth",
                    ClientVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                })
                .WithDesktopFeatures()
                .WithDefaultRedirectUri()
                .Build();

            try
            {
                var accounts = (await application.GetAccountsAsync()).ToList();

                return await application
                    .AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(60000);

                var tokenBuilder = application
                    .AcquireTokenInteractive(scopes)
                    .WithPrompt(Prompt.SelectAccount)
                    .WithClaims(ex.Claims);

                // For PowerShell Core, use system browser instead of embedded web view
                // to avoid window handle issues
                if (IsPowerShellCore())
                {
                    tokenBuilder = tokenBuilder.WithUseEmbeddedWebView(false);
                }

                return await tokenBuilder.ExecuteAsync(cts.Token);
            }
        }
    }
}