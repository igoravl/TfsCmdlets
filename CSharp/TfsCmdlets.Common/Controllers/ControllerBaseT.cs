using System;
using System.Collections.Generic;
using TfsCmdlets.Services;
using System.Composition;

namespace TfsCmdlets.Controllers
{
    public abstract class ControllerBase<T> : ControllerBase, ITypedController<T>
    {
        public override Type DataType => typeof(T);

        public abstract IEnumerable<T> Invoke();

        public override object InvokeCommand() => Invoke();

        protected T GetItem(object parameters = null) => Data.GetItem<T>(parameters);
        
        protected IEnumerable<T> GetItems(object parameters = null) => Data.GetItems<T>(parameters);

        [ImportingConstructor]
        protected ControllerBase(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
   }
}