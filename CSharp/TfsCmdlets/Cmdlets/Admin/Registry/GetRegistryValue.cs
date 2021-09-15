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
    ///   Gets the value of a given Team Foundation Server registry entry.
    /// </summary>
    /// <remarks>
    ///   The 'Get-TfsRegistry' cmdlet retrieves the value of a TFS registry entry at the given path and scope. 
    /// 
    ///   Registry entries can be scoped to the server, to a collection or to a specific user. 
    /// </remarks>
    /// <notes>
    ///   The registry is an internal, hierarchical database that TFS uses to store its 
    ///   configuration and user-level settings and preferences.
    /// 
    ///   IMPORTANT: Retrieving user-scoped values is currently not supported.
    /// </notes>
    /// <example>
    ///   <code>Get-TfsRegistryValue -Path '/Service/Integration/Settings/EmailEnabled'</code>
    ///   <para>Gets the current value of the 'EmailEnabled' key in the TFS Registry</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "TfsRegistryValue")]
    [OutputType(typeof(object))]
    public class GetRegistryValue : CmdletBase
    {
        /// <summary>
        /// Specifies the full path of the TFS Registry key
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Path { get; set; }

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

        // TODO

//        /// <summary>
//        /// Performs execution of the command
//        /// </summary>
//        protected override void DoProcessRecord()
//        {
//            Models.Connection provider;

//            switch (Scope)
//            {
//                case RegistryScope.User: {
//                    throw new NotImplementedException("User scopes are currently not supported");
//                }
//                case RegistryScope.Collection: {
//                    provider = Locator.GetService<Models.TpcConnection>();
//                    break;
//                }
//                case RegistryScope.Server: {
//                    provider = Locator.GetService<Models.ServerConnection>();
//                    break;
//                }
//                default: {
//                    throw new Exception($"Invalid scope {Scope}");
//                }
//            }

//            var soapEnvelope = $@"<s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope'>
//    <s:Body>
//        <QueryRegistryEntries xmlns='http://microsoft.com/webservices/'>
//            <registryPathPattern>{Path}</registryPathPattern>
//        </QueryRegistryEntries>
//    </s:Body>
//</s:Envelope>";

//            var restApiService = Locator.GetService<IRestApiService>();

//            var result = restApiService.InvokeAsync(
//                provider,
//                "/Services/v3.0/RegistryService.asmx",
//                "POST",
//                soapEnvelope,
//                "application/soap+xml",
//                "application/soap+xml",
//                apiVersion: null).SyncResult();

//            var resultString = result.Content.ReadAsStringAsync().GetResult();
//            var resultXml = XDocument.Parse(resultString);
//            var value = resultXml.Descendants(
//                XName.Get("QueryRegistryEntriesResult", "http://microsoft.com/webservices/")).FirstOrDefault()?.Value;

//            WriteObject(value);
//        }
    }
}