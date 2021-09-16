using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IDataManager)), Shared]
    [Export(typeof(ICommandManager))]
    internal class DataManagerImpl : IDataManager, ICommandManager
    {
        private IEnumerable<Lazy<ICommand>> Commands { get; }
        public IParameterManager ParameterManager { get; }
        public Lazy<IConnectionManager> Connections { get; }

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

        public ServerConnection GetServer(object parameters = null)
            => GetItem<ServerConnection>(parameters);

        public TpcConnection GetCollection(object parameters = null)
            => GetItem<TpcConnection>(parameters);

        public WebApiTeamProject GetProject(object parameters = null)
            => GetItem<WebApiTeamProject>(parameters);

        public WebApiTeam GetTeam(object parameters = null)
            => GetItem<WebApiTeam>(parameters);

        [ImportingConstructor]
        public DataManagerImpl(
            [ImportMany] IEnumerable<Lazy<ICommand>> commands,
            IParameterManager parameterManager,
            Lazy<IConnectionManager> connections)
        {
            Commands = commands;
            ParameterManager = parameterManager;
            Connections = connections;
        }
    }
}
