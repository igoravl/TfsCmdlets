//using System;
//using System.Collections.Generic;
//using System.Management.Automation;
//using System.Net;
//using System.Security;
//using Microsoft.VisualStudio.Services.Client;
//using Microsoft.VisualStudio.Services.Common;
//using TfsCmdlets.Models;
//using TfsCmdlets.Services;
//using TfsCmdlets.Util;

//namespace TfsCmdlets.Cmdlets.Credential
//{
//    [Controller(typeof(VssCredentials))]
//    internal class CredentialsController : SimpleControllerBase<VssCredentials>
//    {
//        public CredentialsController(
//            ILogger logger, 
//            IParameterManager parameterManager,
//            IPowerShellService powerShell) 
//            : base(logger, parameterManager, powerShell)
//        {
//        }

//        protected override VssCredentials DoNewItem(ParameterDictionary parameters)
//        {
//            return GetItem(parameters);
//        }

//        protected override IEnumerable<VssCredentials> DoGetItems(ParameterDictionary parameters)
//        {
//            var credential = parameters.Get<object>(nameof(NewCredential.Credential));
//            var userName = parameters.Get<string>(nameof(NewCredential.UserName));
//            var password = parameters.Get<SecureString>(nameof(NewCredential.Password));
//            var accessToken = parameters.Get<string>(nameof(NewCredential.PersonalAccessToken));
//            var interactive = parameters.Get<SwitchParameter>(nameof(NewCredential.Interactive));
//            var url = parameters.Get<Uri>(nameof(NewCredential.Url));

//            var connectionMode = ConnectionMode.CachedCredentials;

//            if (credential != null)
//                connectionMode = ConnectionMode.CredentialObject;
//            else if (!string.IsNullOrEmpty(userName) || password != null)
//                connectionMode = ConnectionMode.UserNamePassword;
//            else if (!string.IsNullOrEmpty(accessToken))
//                connectionMode = ConnectionMode.AccessToken;
//            else if (interactive)
//                connectionMode = ConnectionMode.Interactive;

//            NetworkCredential netCred;

//            while (true) switch (connectionMode)
//            {
//                case ConnectionMode.CachedCredentials:
//                {
//                    Logger.Log("Using cached credentials");

//                    yield return new VssClientCredentials(true);
//                    yield break;
//                }

//                case ConnectionMode.UserNamePassword:
//                {
//                    Logger.Log("Using username/password credentials from UserName/Password arguments");
//                    credential = new NetworkCredential(userName, password);
//                    connectionMode = ConnectionMode.CredentialObject;
//                    continue;
//                }

//                case ConnectionMode.AccessToken:
//                {
//                    Logger.Log("Using credential from supplied Personal Access Token");
//                    credential = new NetworkCredential(string.Empty, accessToken);
//                    connectionMode = ConnectionMode.CredentialObject;
//                    continue;
//                }

//                case ConnectionMode.CredentialObject:
//                {
//                    switch (credential)
//                    {
//                        case VssCredentials cred:
//                        {
//                            Logger.Log("Using supplied credential as-is, since object already is of type VssCredentials");
//                            yield return cred;
//                            yield break;
//                        }
//                        case PSCredential cred:
//                        {
//                            Logger.Log("Using username/password credentials from supplied PSCredential object");
//                            netCred = cred.GetNetworkCredential();
//                            break;
//                        }
//                        case NetworkCredential cred:
//                        {
//                            Logger.Log("Using username/password credentials from supplied NetworkCredential object");
//                            netCred = cred;
//                            break;
//                        }
//                        default:
//                        {
//                            throw new Exception("Invalid argument Credential. Supply either a PowerShell credential (PSCredential object) or a System.Net.NetworkCredential object.");
//                        }
//                    }

//                    yield return IsHosted(url) ?
//                        new VssClientCredentials(
//                            new WindowsCredential(netCred),
//                            new VssBasicCredential(netCred)) :
//                        new VssClientCredentials(
//                            new WindowsCredential(netCred));

//                    yield break;
//                }

//                case ConnectionMode.Interactive:
//                {
//                    if (EnvironmentUtil.PSEdition.Equals("Core"))
//                    {
//                        throw new Exception("Interactive logins are currently not supported in PowerShell Core. Use personal access tokens instead.");
//                    }

//                    Logger.Log("Using interactive credential");

//                    yield return new VssClientCredentials(
//                        new WindowsCredential(false),
//                        new VssFederatedCredential(false),
//                        CredentialPromptType.PromptIfNeeded);
//                    yield break;
//                }

//                default:
//                {
//                    throw new Exception($"Invalid parameter set '{connectionMode}'");
//                }
//            }
//        }

//        private bool IsHosted(Uri url) => (
//            url.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase) ||
//            url.Host.EndsWith(".visualstudio.com", StringComparison.OrdinalIgnoreCase));


//        private enum ConnectionMode
//        {
//            CachedCredentials,
//            CredentialObject,
//            UserNamePassword,
//            AccessToken,
//            Interactive
//        }
//    }
//}