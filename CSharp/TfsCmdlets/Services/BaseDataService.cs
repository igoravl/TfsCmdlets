using System;
using System.Collections.Generic;
using System.Linq;

namespace TfsCmdlets.Services
{
    internal abstract class BaseDataService<T> : BaseService, IDataService<T> where T : class
    {
        public T GetItem() => GetItems()?.FirstOrDefault();

        public IEnumerable<T> GetItems() => DoGetItems();

        public T NewItem() => DoNewItem();

        public void RemoveItem() => DoRemoveItem();

        public T RenameItem() => DoRenameItem();

        public T SuspendItem() => DoSuspendItem();

        public T ResumeItem() => DoResumeItem();

        public T SetItem() => DoSetItem();

        public bool TestItem()
        {
            try { return GetItem() != null; }
            catch { return false; }
        }

        // Protected members

        protected abstract IEnumerable<T> DoGetItems();

        protected virtual T DoNewItem() => throw new NotImplementedException(nameof(DoNewItem));

        protected virtual T DoRenameItem() => throw new NotImplementedException(nameof(DoRenameItem));

        protected virtual void DoRemoveItem() => throw new NotImplementedException(nameof(DoRemoveItem));

        protected virtual T DoSetItem() => throw new NotImplementedException(nameof(DoSetItem));

        protected virtual T DoSuspendItem() => throw new NotImplementedException(nameof(DoSuspendItem));

        protected virtual T DoResumeItem() => throw new NotImplementedException(nameof(DoResumeItem));
   }
}