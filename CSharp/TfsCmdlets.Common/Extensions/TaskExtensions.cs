using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Extensions
{
    public static class TaskExtensions
    {
        public static T GetResult<T>(this Task<T> task, string errorMessage = null)
        {
            try
            {
                task.SyncResult();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{errorMessage?? "Error invoking async operation"} ({ex.Message}. {ex.InnerException?.Message})", ex);
            }
        }

        public static void Wait(this Task task, string errorMessage = null)
        {
            try
            {
                task.Wait();
            }
            catch (Exception ex)
            {
                throw new Exception($"{errorMessage?? "Error invoking async operation"} ({ex.Message}. {ex.InnerException?.Message})", ex);
            }
        }
    }
}
