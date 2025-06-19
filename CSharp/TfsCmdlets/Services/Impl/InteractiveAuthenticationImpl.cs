using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Desktop;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Composition;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IInteractiveAuthentication)), Shared]
    public class InteractiveAuthenticationImpl : IInteractiveAuthentication
    {
        private const string CLIENT_ID = "9f44d9a2-86ef-4794-b2b2-f9038a2628e0";
        private const string SCOPE_ID = "499b84ac-1321-427f-aa17-267ca6975798/user_impersonation";

        private IPowerShellService PowerShell { get; }

        [ImportingConstructor]
        public InteractiveAuthenticationImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }

        /// <summary>
        /// Determines if we're running in PowerShell Core (vs Windows PowerShell)
        /// </summary>
        /// <returns>True if running in PowerShell Core, false otherwise</returns>
        private bool IsPowerShellCore()
        {
            // Use the same detection logic as NewCredential class
            return !PowerShell.Edition.Equals("Desktop", StringComparison.OrdinalIgnoreCase);
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
        private async Task<AuthenticationResult> SignInUserAndGetTokenUsingMSAL(string[] scopes)
        {
            var application = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(new PublicClientApplicationOptions
                {
                    AadAuthorityAudience = AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount,
                    ClientId = CLIENT_ID,
                    ClientName = "TfsCmdlets.InteractiveAuth",
                    ClientVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                })
                .WithWindowsDesktopFeatures(new BrokerOptions(enabledOn: BrokerOptions.OperatingSystems.Windows))
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
                    .WithParentActivityOrWindow(PowerShell.WindowHandle)
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