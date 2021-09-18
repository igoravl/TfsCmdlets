using System;
using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using System.Composition;
using System.Diagnostics;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Commands
{
    internal abstract class CommandBase<T> : CommandBase, IDataCommand<T>
    {
        public override Type DataType => typeof(T);

        public abstract IEnumerable<T> Invoke(ParameterDictionary parameters);

        public override object InvokeCommand(ParameterDictionary parameters) => Invoke(parameters);

        public IDataManager Data { get; }
        public IPowerShellService PowerShell { get; }

        protected TClient GetClient<TClient>() where TClient : VssHttpClientBase
            => Data.GetCollection().GetClient<TClient>();

        protected T GetItem(object parameters = null) => Data.GetItem<T>(parameters);
        
        protected IEnumerable<T> GetItems(object parameters = null) => Data.GetItems<T>(parameters);

        [ImportingConstructor]
        protected CommandBase(IPowerShellService powerShell, IDataManager data, ILogger logger) : base(logger)
        {
            PowerShell = powerShell;
            Data = data;
        }
   }
}