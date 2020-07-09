using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Security;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    /// Provides credentials to use when you connect to a Team Foundation Server or Azure DevOps organization.
    /// </summary>
    [Cmdlet("Get", "TfsCredential", DefaultParameterSetName = "Cached credentials")]
    [OutputType(typeof(VssCredentials))]
    public class GetCredential : CmdletBase
    {
        /// <summary>
        /// Specifies the URL of the server, collection or organization to connect to.
        /// </summary>
        /// <value></value>
        [Parameter(Position=0, Mandatory=true)]
        public Uri Url { get; set; }

        /// <summary>
        /// HELP_PARAM_CACHED_CREDENTIAL
        /// </summary>
        [Parameter(ParameterSetName = "Cached credentials")]
        public SwitchParameter Cached { get; set; }

        /// <summary>
        /// HELP_PARAM_USER_NAME
        /// </summary>
        [Parameter(ParameterSetName = "User name and password", Mandatory = true, Position = 1)]
        public string UserName { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSWORD
        /// </summary>
        [Parameter(ParameterSetName = "User name and password", Position = 2)]
        public SecureString Password { get; set; }

        /// <summary>
        /// HELP_PARAM_CREDENTIAL
        /// </summary>
        [Parameter(ParameterSetName = "Credential object", Mandatory = true)]
        [AllowNull]
        public object Credential { get; set; }

        /// <summary>
        /// HELP_PARAM_PERSONAL_ACCESS_TOKEN
        /// </summary>
        [Parameter(ParameterSetName = "Personal Access Token", Mandatory = true)]
        [Alias("Pat", "PersonalAccessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// HELP_PARAM_INTERACTIVE
        /// </summary>
        [Parameter(ParameterSetName = "Prompt for credential", Mandatory = true)]
        public SwitchParameter Interactive { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            WriteObject(this.GetItem<VssCredentials>());
        }
    }

    [Exports(typeof(VssCredentials))]
    internal class CredentialsService : BaseDataService<VssCredentials>
    {
        protected override IEnumerable<VssCredentials> DoGetItems()
        {
            var credential = GetParameter<object>("Credential");
            var userName = GetParameter<string>("UserName");
            var password = GetParameter<SecureString>("Password");
            var accessToken = GetParameter<string>("AccessToken");
            var interactive = GetParameter<bool>("Interactive");
            var url = GetParameter<Uri>("Url");

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

                        yield return new VssCredentials(true);
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
                            case VssCredentials cred:
                                {
                                    Logger.Log(
                                        "Using supplied credential as-is, since object already is of type VssCredentials");

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

            if(allowInteractive)
                yield return new VssClientCredentials(winCred, fedCred, CredentialPromptType.PromptIfNeeded);
            else
                yield return new VssCredentials(winCred, fedCred, CredentialPromptType.DoNotPrompt);
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