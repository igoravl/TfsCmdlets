using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IAsyncOperationAwaiter))]
    internal class AsyncOperationAwaiterImpl: IAsyncOperationAwaiter
    {
        private IDataManager Data { get; }

        private IOperationsHttpClient OperationsClient { get; }

        public Operation Wait(Task<OperationReference> operation, string errorMessage, int waitTimeInSecs = 2)
        {
            return Wait(operation.GetResult(errorMessage), waitTimeInSecs);
        }

        public Operation Wait(OperationReference operation, int waitTimeInSecs = 2)
        {
            var token = OperationsClient.GetOperation(operation.Id)
                .GetResult("Error getting operation status");
            while (
                (token.Status != OperationStatus.Succeeded) &&
                (token.Status != OperationStatus.Failed) &&
                (token.Status != OperationStatus.Cancelled))
            {
                Thread.Sleep(waitTimeInSecs * 1000);
                token = OperationsClient.GetOperation(operation.Id)
                    .GetResult("Error getting operation status");
            }
            return token;
        }

        [ImportingConstructor]
        public AsyncOperationAwaiterImpl(IDataManager dataManager, IOperationsHttpClient operationsClient)
        {
            Data = dataManager;
            OperationsClient = operationsClient;
        }
    }
}