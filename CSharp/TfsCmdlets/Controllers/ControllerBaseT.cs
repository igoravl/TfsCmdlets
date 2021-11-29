using System;
using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using System.Composition;
using System.Diagnostics;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Controllers
{
    internal abstract class ControllerBase<T> : ControllerBase, ITypedController<T>
    {
        public override Type DataType => typeof(T);

        public abstract IEnumerable<T> Invoke(ParameterDictionary parameters);

        public override object InvokeCommand(ParameterDictionary parameters) => Invoke(parameters);

        public IDataManager Data { get; }

        public IPowerShellService PowerShell { get; }

        protected T GetItem(ParameterDictionary parameters = null) => Data.GetItem<T>(parameters);
        
        protected IEnumerable<T> GetItems(ParameterDictionary parameters = null) => Data.GetItems<T>(parameters);

        [ImportingConstructor]
        protected ControllerBase(IPowerShellService powerShell, IDataManager data, ILogger logger) : base(logger)
        {
            PowerShell = powerShell;
            Data = data;
        }
   }
}