using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.VisualStudio.Services.WebApi;

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
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(object))]
    partial class GetRegistryValue
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
        [Parameter]
        public RegistryScope Scope { get; set; } = RegistryScope.Server;
    }

    [CmdletController]
    partial class GetRegistryValueController 
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

            if(Scope == RegistryScope.Server && provider.IsHosted)
            {
                throw new NotSupportedException("Server scopes are not supported in Azure DevOps Services.");
            }

            var soapEnvelope = $@"<s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope'>
               <s:Body>
                   <QueryRegistryEntries xmlns='http://microsoft.com/webservices/'>
                       <registryPathPattern>{Path}</registryPathPattern>
                   </QueryRegistryEntries>
               </s:Body>
            </s:Envelope>";

            var result = RestApi.InvokeAsync(
                provider,
                "/Services/v3.0/RegistryService.asmx",
                "POST",
                soapEnvelope,
                "application/soap+xml",
                "application/soap+xml",
                apiVersion: null).SyncResult();

            var resultString = result.Content.ReadAsStringAsync().GetResult();
            var resultXml = XDocument.Parse(resultString);
            var value = resultXml.Descendants(
                XName.Get("QueryRegistryEntriesResult", "http://microsoft.com/webservices/")).FirstOrDefault()?.Value;

            return value;
        }
    }
}