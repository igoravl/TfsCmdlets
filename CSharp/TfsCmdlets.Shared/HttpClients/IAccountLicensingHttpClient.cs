using Microsoft.VisualStudio.Services.Licensing.Client;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(AccountLicensingHttpClient))]
    public partial interface IAccountLicensingHttpClient
    {
    }
}