using System;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Admin.Registry
{
    /// <summary>
    ///   Sets the value of a given Team Foundation Server registry entry.
    /// </summary>
    /// <example>
    ///   <code>Get-TfsRegistryValue -Path '/Service/Integration/Settings/EmailEnabled'</code>
    ///   <para>Gets the current value of the 'EmailEnabled' key in the TFS Registry</para>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "TfsRegistryValue", SupportsShouldProcess = true)]
    [OutputType(typeof(object))]
    public class SetRegistryValue : CmdletBase
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
        [Parameter()]
        public RegistryScope Scope { get; set; } = RegistryScope.Server;

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            Models.Connection provider;

            switch (Scope)
            {
                case RegistryScope.User: {
                    throw new NotImplementedException("User scopes are currently not supported");
                }
                case RegistryScope.Collection: {
                    provider = this.GetCollection();
                    break;
                }
                case RegistryScope.Server: {
                    provider = this.GetServer();
                    break;
                }
                default: {
                    throw new Exception($"Invalid scope {Scope}");
                }
            }

            if(!ShouldProcess($"Registry key '{Path}' in {Scope} '{provider}'", $"Set value to '{Value}'"))
                return;

            var soapEnvelope = $@"<s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope'>
    <s:Body>
        <UpdateRegistryEntries xmlns='http://microsoft.com/webservices/'>
            <registryEntries>
                <RegistryEntry Path='{Path}'><Value>{Value}</Value></RegistryEntry>
            </registryEntries>
        </UpdateRegistryEntries>
    </s:Body>
</s:Envelope>";

            var restApiService = GetService<IRestApiService>();

            var result = restApiService.InvokeAsync(
                provider,
                "/Services/v3.0/RegistryService.asmx",
                "POST",
                soapEnvelope,
                "application/soap+xml",
                "application/soap+xml",
                apiVersion: null).SyncResult();
        }
    }
}