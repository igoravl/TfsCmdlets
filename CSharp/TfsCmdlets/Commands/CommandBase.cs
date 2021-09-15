using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Services.CircuitBreaker;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands
{
    internal abstract class CommandBase<T> : ICommand<T>
    {
        protected Models.TpcConnection Collection { get; }

        public virtual string CommandName { get; }

        public virtual string Noun { get; }

        protected CommandBase(Models.TpcConnection collection)
        {
            Collection = collection;
            CommandName = GetCommandName();
        }

        public abstract IEnumerable<T> Invoke(ParameterDictionary parameters);

        public object InvokeCommand(ParameterDictionary parameters)
            => Invoke(parameters);

        private string GetCommandName()
        {
            return GetType().Name;
        }
    }
}