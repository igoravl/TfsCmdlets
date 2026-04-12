using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.HttpClients {
    
    [HttpClient(typeof(OperationsHttpClient))]
    public partial interface IOperationsHttpClient {

    }
}