using System;
using System.Collections.Generic;
using TfsCmdlets.Services;
using System.Composition;

namespace TfsCmdlets.Controllers
{
    internal abstract class ControllerBase<T> : ControllerBase, ITypedController<T>
    {
        public override Type DataType => typeof(T);

        public abstract IEnumerable<T> Invoke();

        public override object InvokeCommand() => Invoke();

        protected IDataManager Data { get; }

        protected IPowerShellService PowerShell { get; }

        protected T GetItem(object parameters = null) => Data.GetItem<T>(parameters);
        
        protected IEnumerable<T> GetItems(object parameters = null) => Data.GetItems<T>(parameters);

        [ImportingConstructor]
        protected ControllerBase(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger) : base(parameters, logger)
        {
            PowerShell = powerShell;
            Data = data;
        }
   }
}