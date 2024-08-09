using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.HttpClients {
    
    [HttpClient(typeof(OperationsHttpClient))]
    partial interface IOperationsHttpClient {

    }
}