using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IDataService<T> : IService where T : class
    {
        T GetItem();

        IEnumerable<T> GetItems();
        
        T NewItem();

        void RemoveItem();

        T RenameItem();

        T SetItem();

        bool TestItem();

        T SuspendItem();

        T ResumeItem();
    }
}