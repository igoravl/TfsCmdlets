using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Extensions
{
    internal static class TaskExtensions
    {
        internal static T GetResult<T>(this Task<T> task, string errorMessage = null)
        {
            try
            {
                task.SyncResult();
                return task.Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{errorMessage?? "Error invoking async operation"}: {ex.Message}");
            }
        }
    }
}
