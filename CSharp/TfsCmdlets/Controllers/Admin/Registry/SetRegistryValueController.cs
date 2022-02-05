using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Controllers.Admin.Registry
{
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

            if(Scope == RegistryScope.Server && provider.IsHosted)
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