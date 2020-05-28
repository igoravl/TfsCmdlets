using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.ServiceProvider;

namespace TfsCmdlets.Services
{
    internal abstract class BaseService : IService
    {
        private ILogService _logger;

        public ICmdletServiceProvider Provider { get; set; }

        public Cmdlet Cmdlet { get; set; }

        protected ILogService Logger => _logger ??= Provider.GetOne<ILogService>(Cmdlet);
    }

    internal abstract class BaseService<T> : BaseService, IService<T>
    {
        protected abstract IEnumerable<T> GetItems(object filter);

        public ParameterDictionary Parameters { get; set; }

        protected abstract string ItemName { get; }

        public T GetOne(object filter = null)
        {
            Parameters ??= Cmdlet.GetParameters();

            var items = GetMany(filter)?.ToList()?? new List<T>();

            if (items.Count != 1 || items[0] == null)
            {
                throw new Exception($"Invalid or non-existent {ItemName}.");
            }

            return items[0];
        }

        public IEnumerable<T> GetMany(object filter = null)
        {
            Parameters ??= Cmdlet.GetParameters();

            return GetItems(filter);
        }
    }
}
