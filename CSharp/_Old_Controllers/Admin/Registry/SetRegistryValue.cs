using System;
using System.Composition;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.Admin.Registry
{
    [CmdletController]
    internal class SetRegistryValue : ControllerBase
    {
        private IPowerShellService PowerShell;
        private IRestApiService RestApi { get; }
        private IDataManager Data { get; }

        public override object InvokeCommand(ParameterDictionary parameters)
        {
            var scope = parameters.Get<RegistryScope>("Scope");
            var path = parameters.Get<string>("Path");
            var value = parameters.Get<object>("Value");
            var provider = scope switch
            {
                RegistryScope.User => throw new NotImplementedException("User scopes are currently not supported"),
                RegistryScope.Collection => Data.GetCollection(),
                RegistryScope.Server => Data.GetServer(parameters),
                _ => throw new Exception($"Invalid scope {scope}")
            };

            if (!PowerShell.ShouldProcess($"Registry key '{path}' in {scope} '{provider}'", $"Set value to '{value}'"))
                return null;

            var soapEnvelope = $@"<s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope'>
                                    <s:Body>
                                        <UpdateRegistryEntries xmlns='http://microsoft.com/webservices/'>
                                            <registryEntries>
                                                <RegistryEntry Path='{path}'><Value>{value}</Value></RegistryEntry>
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

        [ImportingConstructor]
        public SetRegistryValue(IPowerShellService powerShell, IRestApiService restApi, IDataManager data, ILogger logger)
         : base(logger)
        {
            PowerShell = powerShell;
            RestApi = restApi;
            Data = data;
        }
    }
}