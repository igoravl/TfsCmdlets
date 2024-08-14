namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(GenericHttpClient))]
    partial interface IGenericHttpClient
    {
    }
}