using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Management.Automation;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IDataManager)), Shared]
    [Export(typeof(ICommandManager))]
    internal class DataManagerImpl : IDataManager, ICommandManager
    {
        private IEnumerable<Lazy<ICommand>> Commands { get; }
        public IParameterManager ParameterManager { get; }

        public T GetItem<T>(object parameters = null)
        {
            return GetItems<T>(parameters).First();
        }

        public IEnumerable<T> GetItems<T>(object parameters = null)
        {
            return Invoke<T>(VerbsCommon.Get, parameters);
        }

        public IEnumerable<T> Invoke<T>(string verb, object parameters = null)
        {
            var dataType = typeof(T);
            var command = Commands.FirstOrDefault(c => c.Value.Verb == verb && c.Value.DataType.Equals(dataType)).Value as IDataCommand<T>;

            if (command == null)
            {
                throw new ArgumentException($"Command Get not found for data type {dataType.Name}");
            }

            return command.Invoke(ParameterManager.GetParameters(parameters));
        }

        public ICommand GetCommandByName(string commandName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICommand> GetCommandByVerb(string verb, string noun = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICommand> GetCommandByNoun(string noun)
        {
            throw new NotImplementedException();
        }

        [ImportingConstructor]
        public DataManagerImpl(
            [ImportMany] IEnumerable<Lazy<ICommand>> commands, 
            IParameterManager parameterManager)
        {
            Commands = commands;
            ParameterManager = parameterManager;
        }

    }
}
