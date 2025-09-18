using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ExtensionManagementHttpClient))]
    public partial interface IExtensionManagementHttpClient
    {
    }
}