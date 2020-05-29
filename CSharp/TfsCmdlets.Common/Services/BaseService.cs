using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
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

        protected object ItemFilter { get; set; }

        public T GetOne(ParameterDictionary overriddenParameters, object userState = null)
        {
            Parameters = (overriddenParameters ?? new ParameterDictionary());
            Parameters.Merge(new ParameterDictionary(Cmdlet));

            var items = GetMany(overriddenParameters, userState)?.ToList()?? new List<T>();
            if(items == null || items.Count == 0) throw new Exception($"Invalid or non-existent {ItemName} '{ItemFilter}'");

            return items[0];
        }

        public IEnumerable<T> GetMany(ParameterDictionary overriddenParameters, object userState = null)
        {
            Parameters = (overriddenParameters ?? new ParameterDictionary());
            Parameters.Merge(new ParameterDictionary(Cmdlet));

            return GetItems(userState);
        }
    }
}
