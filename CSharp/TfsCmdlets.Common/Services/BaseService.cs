using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.ServiceProvider;

namespace TfsCmdlets.Services
{
    internal abstract class BaseService<T>: IService<T>
    {
        private ILogService _logger;

        public ICmdletServiceProvider Provider { get; set; }

        public Cmdlet Cmdlet { get; set; }

        public ParameterDictionary Parameters { get; set; }

        protected abstract string ItemName { get; }

        protected abstract IEnumerable<T> GetItems(object filter);

        public T GetOne(object filter = null)
        {
            Parameters ??= Cmdlet.GetParameters();

            var items = GetMany(filter)?.ToList()?? new List<T>();

            if (items.Count != 1)
            {
                throw new Exception($"Invalid {ItemName}(s) '{filter}'");
            }

            return items[0];
        }

        public IEnumerable<T> GetMany(object filter = null)
        {
            Parameters ??= Cmdlet.GetParameters();

            return GetItems(filter);
        }

        protected ILogService Logger => _logger ??= Provider.GetService<ILogService>(Cmdlet).GetOne();
    }
}
