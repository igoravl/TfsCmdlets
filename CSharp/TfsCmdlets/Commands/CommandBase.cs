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

        public IConnectionManager Connections { get; }

        protected TClient GetClient<TClient>() where TClient : VssHttpClientBase
        {
            return Connections.GetCollection().GetClient<TClient>();
        }

        [ImportingConstructor]
        protected CommandBase(IConnectionManager connections, IDataManager data, ILogger logger) : base(logger)
        {
            Connections = connections;
            Data = data;

            Debug.WriteLine($"{GetType().Name} created");
        }
   }
}