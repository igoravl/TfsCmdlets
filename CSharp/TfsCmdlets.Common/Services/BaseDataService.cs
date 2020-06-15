using System;
using System.Collections.Generic;
using System.Linq;

namespace TfsCmdlets.Services
{
    internal abstract class BaseDataService<T> : BaseService, IDataService<T> where T : class
    {
        public T GetItem()
        {
            var items = GetItems()?.ToList() ?? new List<T>();
            if (items == null || items.Count == 0) return null;

            return items[0];
        }

        public bool TestItem()
        {
            try { return GetItem() != null; }
            catch { return false; }
        }

        public IEnumerable<T> GetItems() => DoGetItems();

        public T NewItem() => DoNewItem();

        public void RemoveItem() => DoRemoveItem();

        public T RenameItem(T item, string newName) => DoRenameItem(item, newName);

        public T SetItem(T item) => DoSetItem(item);

        // Protected members

        protected abstract IEnumerable<T> DoGetItems();

        protected virtual T DoNewItem() => throw new NotImplementedException(nameof(DoNewItem));

        private T DoRenameItem(T item, string newName) => throw new NotImplementedException(nameof(DoRenameItem));

        private void DoRemoveItem() => throw new NotImplementedException(nameof(DoRemoveItem));

        private T DoSetItem(T item) => throw new NotImplementedException(nameof(DoSetItem));

    }
}