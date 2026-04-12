namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(GenericHttpClient))]
    public partial interface IGenericHttpClient
    {
    }
}