using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Cmdlets.Admin.Registry
{
    /// <summary>
    ///   Sets the value of a given Team Foundation Server registry entry.
    /// </summary>
    /// <remarks>
    ///   The 'Set-TfsRegistry' cmdlet changes the value of a TFS registry key to the 
    ///   value specified in the command.
    /// </remarks>
    /// <example>
    ///   <code>Get-TfsRegistryValue -Path '/Service/Integration/Settings/EmailEnabled'</code>
    ///   <para>Gets the current value of the 'EmailEnabled' key in the TFS Registry</para>
    /// </example>
    /// <notes>
    ///   The registry is an internal, hierarchical database that TFS uses to store its 
    ///   configuration and user-level settings and preferences.
    /// 
    ///   IMPORTANT: Retrieving user-scoped values is currently not supported.
    /// </notes>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(object))]
    partial class SetRegistryValue
    {
        /// <summary>
        /// Specifies the full path of the TFS Registry key
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Path { get; set; }

        /// <summary>
        /// Specifies the new value of the Registry key. To remove an existing value, 
        /// set it to $null
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        [AllowNull, AllowEmptyString]
        public string Value { get; set; }

        /// <summary>
        /// Specifies the scope under which to search for the key. 
        /// When omitted, defaults to the Server scope.
        /// </summary>
        [Parameter]
        public RegistryScope Scope { get; set; } = RegistryScope.Server;
    }

    [CmdletController]
    partial class SetRegistryValueController
    {
        [Import]
        private IRestApiService RestApi { get; }

        protected override IEnumerable Run()
        {
            var provider = Scope switch
            {
                RegistryScope.User => throw new NotImplementedException("User scopes are currently not supported"),
                RegistryScope.Collection => Collection,
                RegistryScope.Server => Server,
                _ => throw new Exception($"Invalid scope {Scope}")
            };

            if (Scope == RegistryScope.Server && provider.IsHosted)
            {
                throw new NotSupportedException("Server scopes are not supported in Azure DevOps Services.");
            }

            if (!PowerShell.ShouldProcess($"Registry key '{Path}' in {Scope} '{provider}'", $"Set value to '{Value}'"))
                return null;

            var soapEnvelope = $@"<s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope'>
                                    <s:Body>
                                        <UpdateRegistryEntries xmlns='http://microsoft.com/webservices/'>
                                            <registryEntries>
                                                <RegistryEntry Path='{Path}'><Value>{Value}</Value></RegistryEntry>
                                            </registryEntries>
                                        </UpdateRegistryEntries>
                                    </s:Body>
                                </s:Envelope>";

            RestApi.InvokeAsync(
                provider,
                "/Services/v3.0/RegistryService.asmx",
                "POST",
                soapEnvelope,
                "application/soap+xml",
                "application/soap+xml",
                apiVersion: null).SyncResult();

            return null;
        }
    }
}