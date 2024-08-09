using Microsoft.VisualStudio.Services.Licensing.Client;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(AccountLicensingHttpClient))]
    partial interface IAccountLicensingHttpClient
    {
    }
}