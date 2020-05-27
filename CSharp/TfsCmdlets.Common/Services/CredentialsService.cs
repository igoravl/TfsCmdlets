using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Services
{
    [Exports(typeof(VssClientCredentials))]
    internal class CredentialsService : BaseService<VssClientCredentials>
    {
        protected override string ItemName => "Credential";

        protected override IEnumerable<VssClientCredentials> GetItems(object filter)
        {
            var parms = Cmdlet.GetParameters();

            var credential = parms["Credential"];
            var userName = parms["UserName"] as string;
            var password = parms["Password"] as string;
            var accessToken = parms["AccessToken"] as string;
            var interactive = (bool) parms["Interactive"];
            var parameterSetName = ConnectionMode.CachedCredentials;

            if (credential != null)
                parameterSetName = ConnectionMode.CredentialObject;
            else if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password))
                parameterSetName = ConnectionMode.UserNamePassword;
            else if (!string.IsNullOrEmpty(accessToken))
                parameterSetName = ConnectionMode.AccessToken;
            else if (interactive)
                parameterSetName = ConnectionMode.Interactive;

            var allowInteractive = false;
            NetworkCredential netCred = null;
            FederatedCredential fedCred = null;
            WindowsCredential winCred = null;

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