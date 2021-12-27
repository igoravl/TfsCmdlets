using System.Xml.Linq;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Controllers.Admin.Registry
{
    [CmdletController]
    partial class GetRegistryValueController 
    {
        [Import]
        private IRestApiService RestApi { get; }

        public override object InvokeCommand()
        {
            var scope = Parameters.Get<RegistryScope>("Scope");
            var path = Parameters.Get<string>("Path");

            var provider = scope switch
            {
                RegistryScope.User => throw new NotImplementedException("User scopes are currently not supported"),
                RegistryScope.Collection => Data.GetCollection(),
                RegistryScope.Server => Data.GetServer(),
                _ => throw new Exception($"Invalid scope {scope}")
            };

            if(scope == RegistryScope.Server && provider.IsHosted)
            {
                throw new NotSupportedException("Server scopes are not supported in Azure DevOps Services.");
            }

            var soapEnvelope = $@"<s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope'>
               <s:Body>
                   <QueryRegistryEntries xmlns='http://microsoft.com/webservices/'>
                       <registryPathPattern>{path}</registryPathPattern>
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