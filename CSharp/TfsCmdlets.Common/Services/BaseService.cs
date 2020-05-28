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
        private ILogger _logger;

        public ICmdletServiceProvider Provider { get; set; }

        public Cmdlet Cmdlet { get; set; }

        protected ILogger Logger => _logger ??= new LoggerImpl(Cmdlet);
    }

    internal abstract class BaseDataService<T> : BaseService, IDataService<T>
    {
        protected abstract IEnumerable<T> GetItems(object userState);

        public ParameterDictionary Parameters { get; set; }

        protected abstract string ItemName { get; }

        public T GetOne(ParameterDictionary overriddenParameters, object userState = null)
        {
            Parameters = (overriddenParameters ?? new ParameterDictionary());
            Parameters.Merge(Cmdlet.GetParameters());

            var items = GetMany(overriddenParameters, userState)?.ToList()?? new List<T>();

            if (items.Count != 1 || items[0] == null)
            {
                throw new Exception($"Invalid or non-existent {ItemName}.");
            }

            return items[0];
        }

        public IEnumerable<T> GetMany(ParameterDictionary overriddenParameters, object userState = null)
        {
            Parameters = (overriddenParameters ?? new ParameterDictionary());
            Parameters.Merge(Cmdlet.GetParameters());

            return GetItems(userState);
        }
    }
}
