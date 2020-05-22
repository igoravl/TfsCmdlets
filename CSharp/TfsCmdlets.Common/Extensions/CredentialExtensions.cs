using System;
using System.Management.Automation;
using System.Net;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Extensions
{
    internal static class CredentialExtensions
    {
        internal static VssClientCredentials GetCredential(this PSCmdlet cmdlet)
        {
            var parms = cmdlet.GetParameters();

            var parameterSetName = parms["ParameterSetName"];
            var credential = parms["Credential"];
            var userName = parms["UserName"] as string;
            var password = parms["Password"] as string;
            var accessToken = parms["AccessToken"] as string;

            if (parameterSetName.Equals("Credential object") && credential == null)
                parameterSetName = "Cached Credentials";

            var allowInteractive = false;
            NetworkCredential netCred;
            FederatedCredential fedCred;
            WindowsCredential winCred;

            switch (parameterSetName)
            {
                case "Cached credentials":
                    {
                        cmdlet.Log("Using cached credentials");

                        return new VssClientCredentials(true);
                    }

                case "User name and password":
                    {
                        cmdlet.Log("Using username/password credentials from UserName/Password arguments");

                        netCred = new NetworkCredential(userName, password);
                        fedCred = new VssBasicCredential(netCred);
                        winCred = new WindowsCredential(netCred);

                        break;
                    }

                case "Credential object":
                    {
                        switch (credential)
                        {
                            case VssClientCredentials cred:
                                {
                                    cmdlet.Log("Using supplied credential as-is, since object already is of type VssClientCredentials");

                                    return cred;
                                }

                            case PSCredential cred:
                                {
                                    cmdlet.Log("Using username/password credentials from supplied PSCredential object");

                                    netCred = cred.GetNetworkCredential();

                                    break;
                                }

                            case NetworkCredential cred:
                                {
                                    cmdlet.Log("Using username/password credentials from supplied NetworkCredential object");

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

                case "Personal Access Token":
                    {
                        cmdlet.Log("Using credential from supplied Personal Access Token");

                        netCred = new NetworkCredential("dummy-pat-user", accessToken);
                        fedCred = new VssBasicCredential(netCred);
                        winCred = new WindowsCredential(netCred);

                        break;
                    }

                case "Prompt for credential":
                    {
                        if (cmdlet.GetVariableValue("PSEdition").Equals("Core"))
                            throw new Exception(
                                "Interactive logins are currently not supported in PowerShell Core. Use personal access tokens instead.");

                        cmdlet.Log("Using interactive credential");

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

            return new VssClientCredentials(winCred, fedCred, promptType);
        }
    }
}