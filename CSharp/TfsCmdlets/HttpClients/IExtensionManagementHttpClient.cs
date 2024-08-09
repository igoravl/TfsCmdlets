using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ExtensionManagementHttpClient))]
    partial interface IExtensionManagementHttpClient
    {
    }
}