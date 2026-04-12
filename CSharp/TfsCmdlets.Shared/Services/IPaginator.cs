using System.Management.Automation;
using System.Threading.Tasks;

namespace TfsCmdlets.Services
{
    public interface IPaginator
    {
        IEnumerable<T> Paginate<T>(Func<int, int, IEnumerable<T>> enumerable, int pageSize = 100);

        IEnumerable<T> Paginate<T>(Func<int, int, Task<IEnumerable<T>>> enumerable, string errorMessage, int pageSize = 100);

        // IEnumerable<T> Paginate<T>(Func<int, int, Task<List<T>>> enumerable, string errorMessage, int pageSize = 100);
    }
}