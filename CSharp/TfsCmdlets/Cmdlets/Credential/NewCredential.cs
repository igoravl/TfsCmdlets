using System.Management.Automation;
using Microsoft.VisualStudio.Services.Common;
using System.Net;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.OAuth;

namespace TfsCmdlets.Cmdlets.Credential
{
    /// <summary>
    /// Provides credentials to use when you connect to a Team Foundation Server 
    /// or Azure DevOps organization.
    /// </summary>
    [TfsCmdlet(CmdletScope.None, DefaultParameterSetName = "Cached credentials", OutputType = typeof(VssCredentials), 
        CustomControllerName = "GetCredential", ReturnsValue = true)]
    partial class NewCredential 
    {
        /// <summary>
        /// Specifies the URL of the server, collection or organization to connect to.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public Uri Url { get; set; }
    }

    [CmdletController(typeof(VssCredentials), CustomCmdletName = "NewCredential")]
    partial class GetCredentialController
    {
        [Import]
        private IInteractiveAuthentication InteractiveAuthentication { get; }

        protected override IEnumerable Run()
        {
            var connectionMode = ConnectionMode.CachedCredentials;

            if (Has_Credential)
                connectionMode = ConnectionMode.CredentialObject;
            else if (Has_UserName && Has_Password)
                connectionMode = ConnectionMode.UserNamePassword;
            else if (Has_PersonalAccessToken)
                connectionMode = ConnectionMode.AccessToken;
            else if (Interactive)
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
                        netCred = new NetworkCredential(UserName, Password);
                        break;
                    }

                case ConnectionMode.AccessToken:
                    {
                        Logger.Log("Using credential from supplied Personal Access Token");
                        netCred = new NetworkCredential(string.Empty, PersonalAccessToken);
                        break;
                    }

                case ConnectionMode.CredentialObject:
                    {
                        switch (Credential)
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
                        Logger.Log("Using interactive credential");

                        if (PowerShell.Edition.Equals("Desktop"))
                        {
                            // Windows PowerShell 

                            yield return new VssClientCredentials(
                                new WindowsCredential(false),
                                new VssFederatedCredential(false),
                                CredentialPromptType.PromptIfNeeded);

                            yield break;
                        }

                        if (IsHosted(Url))
                        {
                            // PowerShell Core

                            yield return new VssCredentials(
                                new VssOAuthAccessTokenCredential(InteractiveAuthentication.GetToken(Url)));
                            yield break;
                        }

                        throw new Exception("Interactive authentication is not supported for TFS / Azure DevOps Server in PowerShell Core. Please use either a username/password credential or a Personal Access Token.");
                    }

                default:
                    {
                        throw new Exception($"Invalid parameter set '{connectionMode}'");
                    }
            }

            if (netCred == null) yield break;

            yield return IsHosted(Url) ?
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