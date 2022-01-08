using System.Management.Automation;
using System.Net;
using System.Security;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using TfsCmdlets.Cmdlets.Credential;

namespace TfsCmdlets.Controllers.Credential
{
    [CmdletController(typeof(VssCredentials), CustomCmdletName = "NewCredential")]
    partial class GetCredentialController
    {
        public override IEnumerable<VssCredentials> Invoke()
        {
            var credential = Parameters.Get<object>(nameof(NewCredential.Credential));
            var userName = Parameters.Get<string>(nameof(NewCredential.UserName));
            var password = Parameters.Get<SecureString>(nameof(NewCredential.Password));
            var accessToken = Parameters.Get<string>(nameof(NewCredential.PersonalAccessToken));
            var interactive = Parameters.Get<bool>(nameof(NewCredential.Interactive));
            var url = Parameters.Get<Uri>(nameof(NewCredential.Url));

            var connectionMode = ConnectionMode.CachedCredentials;

            if (credential != null)
                connectionMode = ConnectionMode.CredentialObject;
            else if (!string.IsNullOrEmpty(userName) || password != null)
                connectionMode = ConnectionMode.UserNamePassword;
            else if (!string.IsNullOrEmpty(accessToken))
                connectionMode = ConnectionMode.AccessToken;
            else if (interactive)
                connectionMode = ConnectionMode.Interactive;

            NetworkCredential netCred = null;

            switch (connectionMode)
            {
                case ConnectionMode.CachedCredentials:
                    {
                        Logger.Log("Using cached credentials");

                        yield return new VssClientCredentials(true);
                        break;
                    }

                case ConnectionMode.UserNamePassword:
                    {
                        Logger.Log("Using username/password credentials from UserName/Password arguments");
                        netCred = new NetworkCredential(userName, password);
                        break;
                    }

                case ConnectionMode.AccessToken:
                    {
                        Logger.Log("Using credential from supplied Personal Access Token");
                        netCred = new NetworkCredential(string.Empty, accessToken);
                        break;
                    }

                case ConnectionMode.CredentialObject:
                    {
                        switch (credential)
                        {
                            case VssCredentials cred:
                                {
                                    Logger.Log("Using supplied credential as-is, since object already is of type VssCredentials");
                                    yield return cred;
                                    break;
                                }
                            case PSCredential cred:
                                {
                                    Logger.Log("Using username/password credentials from supplied PSCredential object");
                                    netCred = cred.GetNetworkCredential();
                                    break;
                                }
                            case NetworkCredential cred:
                                {
                                    Logger.Log("Using username/password credentials from supplied NetworkCredential object");
                                    netCred = cred;
                                    break;
                                }
                            default:
                                {
                                    throw new Exception("Invalid argument Credential. Supply either a PowerShell credential (PSCredential object) or a System.Net.NetworkCredential object.");
                                }
                        }
                        break;
                    }

                case ConnectionMode.Interactive:
                    {
                        if (PowerShell.Edition.Equals("Core"))
                        {
                            throw new Exception("Interactive logins are currently not supported in PowerShell Core. Use personal access tokens instead.");
                        }

                        Logger.Log("Using interactive credential");

                        yield return new VssClientCredentials(
                            new WindowsCredential(false),
                            new VssFederatedCredential(false),
                            CredentialPromptType.PromptIfNeeded);
                        break;
                    }

                default:
                    {
                        throw new Exception($"Invalid parameter set '{connectionMode}'");
                    }
            }

            if (netCred == null) yield break;

            yield return IsHosted(url) ?
                new VssClientCredentials(
                    new WindowsCredential(netCred),
                    new VssBasicCredential(netCred)) :
                new VssClientCredentials(
                    new WindowsCredential(netCred));
        }

        private bool IsHosted(Uri url) => (
            url.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase) ||
            url.Host.EndsWith(".visualstudio.com", StringComparison.OrdinalIgnoreCase));

        private enum ConnectionMode
        {
            CachedCredentials,
            CredentialObject,
            UserNamePassword,
            AccessToken,
            Interactive
        }
    }
}