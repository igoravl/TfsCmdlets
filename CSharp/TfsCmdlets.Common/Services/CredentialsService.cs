using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Security;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Services
{
    [Exports(typeof(VssClientCredentials))]
    internal class CredentialsService : BaseDataService<VssClientCredentials>
    {
        protected override IEnumerable<VssClientCredentials> DoGetItems()
        {
            var credential = GetParameter<object>("Credential");
            var userName = GetParameter<string>("UserName");
            var password = GetParameter<SecureString>("Password");
            var accessToken = GetParameter<string>("AccessToken");
            var interactive = GetParameter<bool>("Interactive");
            var parameterSetName = ConnectionMode.CachedCredentials;

            if (credential != null)
                parameterSetName = ConnectionMode.CredentialObject;
            else if (!string.IsNullOrEmpty(userName) || password != null)
                parameterSetName = ConnectionMode.UserNamePassword;
            else if (!string.IsNullOrEmpty(accessToken))
                parameterSetName = ConnectionMode.AccessToken;
            else if (interactive)
                parameterSetName = ConnectionMode.Interactive;

            var allowInteractive = false;

            NetworkCredential netCred;
            FederatedCredential fedCred;
            WindowsCredential winCred;

            switch (parameterSetName)
            {
                case ConnectionMode.CachedCredentials:
                {
                    Logger.Log("Using cached credentials");

                    yield return new VssClientCredentials(true);
                    yield break;
                }

                case ConnectionMode.UserNamePassword:
                {
                    Logger.Log("Using username/password credentials from UserName/Password arguments");

                    netCred = new NetworkCredential(userName, password);
                    fedCred = new VssBasicCredential(netCred);
                    winCred = new WindowsCredential(netCred);

                    break;
                }

                case ConnectionMode.CredentialObject:
                {
                    switch (credential)
                    {
                        case VssClientCredentials cred:
                        {
                            Logger.Log(
                                "Using supplied credential as-is, since object already is of type VssClientCredentials");

                            yield return cred;
                            yield break;
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
                            throw new Exception(
                                "Invalid argument Credential. Supply either a PowerShell credential (PSCredential object) or a System.Net.NetworkCredential object.");
                        }
                    }

                    fedCred = new VssBasicCredential(netCred);
                    winCred = new WindowsCredential(netCred);

                    break;
                }

                case ConnectionMode.AccessToken:
                {
                    Logger.Log("Using credential from supplied Personal Access Token");

                    netCred = new NetworkCredential("dummy-pat-user", accessToken);
                    fedCred = new VssBasicCredential(netCred);
                    winCred = new WindowsCredential(netCred);

                    break;
                }

                case ConnectionMode.Interactive:
                {
                    if (EnvironmentUtil.PSEdition.Equals("Core"))
                        throw new Exception(
                            "Interactive logins are currently not supported in PowerShell Core. Use personal access tokens instead.");

                    Logger.Log("Using interactive credential");

                    fedCred = new VssFederatedCredential(false);
                    winCred = new WindowsCredential(false);
                    allowInteractive = true;

                    break;
                }

                default:
                {
                    throw new Exception($"Invalid parameter set '{parameterSetName}'");
                }
            }

            var promptType = allowInteractive ? CredentialPromptType.PromptIfNeeded : CredentialPromptType.DoNotPrompt;

            yield return new VssClientCredentials(winCred, fedCred, promptType);
        }

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