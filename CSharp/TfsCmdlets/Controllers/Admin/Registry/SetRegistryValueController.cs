using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Controllers.Admin.Registry
{
    [CmdletController]
    partial class SetRegistryValueController
    {
        [Import]
        private IRestApiService RestApi { get; }

        public override object InvokeCommand()
        {
            var scope = Parameters.Get<RegistryScope>("Scope");
            var path = Parameters.Get<string>("Path");
            var value = Parameters.Get<object>("Value");
            var provider = scope switch
            {
                RegistryScope.User => throw new NotImplementedException("User scopes are currently not supported"),
                RegistryScope.Collection => Data.GetCollection(),
                RegistryScope.Server => Data.GetServer(),
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
    }
}