using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.Services
{
    public interface IAsyncOperationAwaiter
    {
        (OperationStatus, string) Wait(Task<OperationReference> operation, string errorMessage, int waitTimeInSecs = 2);
        (OperationStatus, string) Wait(OperationReference operation, int waitTimeInSecs = 2);
    }
}