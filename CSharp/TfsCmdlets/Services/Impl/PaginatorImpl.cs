using System.Threading.Tasks;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IPaginator))]
    public class Paginator : IPaginator
    {
        public IEnumerable<T> Paginate<T>(Func<int, int, Task<IEnumerable<T>>> enumerable, string errorMessage, int pageSize)
        {
            var isReturning = true;
            var loop = 0;
            int items;

            while (isReturning)
            {
                isReturning = false;
                items = 0;

                foreach (var result in enumerable(pageSize, loop++ * pageSize).GetResult(errorMessage))
                {
                    items++;
                    isReturning = true;
                    yield return result;
                }

                isReturning = isReturning && (items == pageSize);
            }
        }

        public IEnumerable<T> Paginate<T>(Func<int, int, IEnumerable<T>> enumerable, int pageSize)
        {
            var isReturning = true;
            var loop = 0;
            int items;

            while (isReturning)
            {
                isReturning = false;
                items = 0;

                foreach (var result in enumerable(pageSize, loop++ * pageSize))
                {
                    items++;
                    isReturning = true;
                    yield return result;
                }

                isReturning = isReturning && (items == pageSize);
            }
        }
    }
}
