using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.Services
{
    public interface IAsyncOperationAwaiter
    {
        Operation Wait(Task<OperationReference> operation, string errorMessage, int waitTimeInSecs = 2);
        Operation Wait(OperationReference operation, int waitTimeInSecs = 2);
    }
}