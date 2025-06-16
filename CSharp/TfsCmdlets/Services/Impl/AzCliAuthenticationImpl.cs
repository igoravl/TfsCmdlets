using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IAzCliAuthentication)), Shared]
    public class AzCliAuthenticationImpl : IAzCliAuthentication
    {
        private const string AzureDevOpsResourceId = "499b84ac-1321-427f-aa17-267ca6975798";

        public string GetToken(Uri uri, bool useMsi = false)
        {
            var token = useMsi ? GetMsiTokenAsync().Result : GetAzCliTokenAsync().Result;
            return token;
        }

        private async Task<string> GetAzCliTokenAsync()
        {
            var credential = new DefaultAzureCredential();
            var tokenRequestContext = new TokenRequestContext(new[] { $"{AzureDevOpsResourceId}/.default" });
            var token = await credential.GetTokenAsync(tokenRequestContext);
            return token.Token;
        }

        private async Task<string> GetMsiTokenAsync()
        {
            var credential = new ManagedIdentityCredential();
            var tokenRequestContext = new TokenRequestContext(new[] { $"{AzureDevOpsResourceId}/.default" });
            var token = await credential.GetTokenAsync(tokenRequestContext);
            return token.Token;
        }
    }
}
