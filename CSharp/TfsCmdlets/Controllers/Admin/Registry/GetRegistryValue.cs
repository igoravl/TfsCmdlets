using System;
using System.Composition;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Controllers;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.Admin.Registry
{
    [CmdletController]
    internal class GetRegistryValue : ControllerBase
    {
        private IPowerShellService PowerShell { get; }
        public IRestApiService RestApi { get; }
        private IDataManager Data { get; }

        public override object InvokeCommand(ParameterDictionary parameters)
        {
            var scope = parameters.Get<RegistryScope>("Scope");
            var path = parameters.Get<string>("Path");
            var provider = scope switch
            {
                RegistryScope.User => throw new NotImplementedException("User scopes are currently not supported"),
                RegistryScope.Collection => Data.GetCollection(parameters),
                RegistryScope.Server => Data.GetServer(parameters),
                _ => throw new Exception($"Invalid scope {scope}")
            };

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

        [ImportingConstructor]
        public GetRegistryValue(IRestApiService restApi, IDataManager data, ILogger logger)
         : base(logger)
        {
            RestApi = restApi;
            Data = data;
        }
    }
}