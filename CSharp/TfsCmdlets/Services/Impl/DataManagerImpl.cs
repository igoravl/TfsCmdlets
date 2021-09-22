using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Util;
using Microsoft.VisualStudio.Services.Common;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IDataManager)), Shared]
    // [Export(typeof(ICommandManager))]
    internal class DataManagerImpl : IDataManager //, ICommandManager
    {
        protected IEnumerable<Lazy<ICommand>> Commands { get; }
        protected IParameterManager ParameterManager { get; }
        protected ILogger Logger { get; }
        protected ICurrentConnections CurrentConnections { get; }

        public IEnumerable<T> Invoke<T>(string verb, object parameters)
        {
            var dataType = typeof(T);
            var command = Commands.FirstOrDefault(c => c.Value.Verb == verb && c.Value.DataType.Equals(dataType)).Value as IDataCommand<T>;

            if (command == null)
            {
                throw new ArgumentException($"Command {verb} not found for data type {dataType.Name}");
            }

            return command.Invoke(ParameterManager.GetParameters(parameters));
        }

        // public ICommand GetCommandByName(string commandName)
        // {
        //     throw new NotImplementedException();
        // }

        // public IEnumerable<ICommand> GetCommandByVerb(string verb, string noun = null)
        // {
        //     throw new NotImplementedException();
        // }

        // public IEnumerable<ICommand> GetCommandByNoun(string noun)
        // {
        //     throw new NotImplementedException();
        // }

        public T GetItem<T>(object parameters)
            => GetItems<T>(parameters).First();

        public IEnumerable<T> GetItems<T>(object parameters)
            => Invoke<T>(VerbsCommon.Get, parameters);

        public Connection GetServer(object parameters)
            => CreateConnection(ClientScope.Server, parameters);

        public Connection GetCollection(object parameters)
            => CreateConnection(ClientScope.Collection, parameters);

        public WebApiTeamProject GetProject(object parameters)
            => GetItem<WebApiTeamProject>(parameters);

        public WebApiTeam GetTeam(object parameters)
            => GetItem<WebApiTeam>(parameters);

        public T GetClient<T>(object parameters)
            => (T) ((ITfsServiceProvider)GetCollection(parameters)).GetClient(typeof(T));

        public T GetService<T>(object parameters)
            => (T) ((ITfsServiceProvider)GetCollection(parameters)).GetService(typeof(T));

        private Connection CreateConnection(ClientScope scope, object parameters)
        {
#if NETCOREAPP3_1_OR_GREATER
            Microsoft.VisualStudio.Services.WebApi.VssConnection result = null;
#else
            Microsoft.TeamFoundation.Client.TfsConnection result = null;
#endif
            var parms = ParameterManager.GetParameters(parameters);
            var connection = parms.Get<object>(scope.ToString());
            var current = parms.Get<bool>("Current");

            while (result == null) switch (connection)
            {
                case Connection conn:
                {
                    result = conn.InnerConnection;
                    break;
                }
#if NETCOREAPP3_1_OR_GREATER
                case Microsoft.VisualStudio.Services.WebApi.VssConnection conn:
                {
                    result = conn;
                    break;
                }
                case Uri uri:
                {
                    Logger.Log($"Get {scope} referenced by URL '{uri}'");
                    result = new Microsoft.VisualStudio.Services.WebApi.VssConnection(
                        uri, GetItem<VssCredentials>(new { Url = uri }));
                    break;
                }
#else
                case Microsoft.TeamFoundation.Client.TfsConnection conn:
                    {
                        result = conn;
                        break;
                    }
                case Uri uri when scope == ClientScope.Server:
                    {
                        Logger.Log($"Get {scope} referenced by URL '{uri}'");
                        result = new Microsoft.TeamFoundation.Client.TfsConfigurationServer(uri, GetItem<VssCredentials>(new { Url = uri }));
                        break;
                    }
                case Uri uri when scope == ClientScope.Collection:
                    {
                        Logger.Log($"Get {scope} referenced by URL '{uri}'");
                        result = Microsoft.TeamFoundation.Client.TfsTeamProjectCollectionFactory.GetTeamProjectCollection(
                            uri, GetItem<VssCredentials>(new { Url = uri }));
                        break;
                    }
#endif
                    case null:
                {
                    Logger.Log($"Get currently connected {scope}");
                    result = ((Connection)CurrentConnections.Get(scope.ToString()))?.InnerConnection;

                    if (result == null && current) return null;

                    ErrorUtil.ThrowIfNull(result, scope.ToString(), $"No connection information available. Either supply a valid -{scope} argument or use one of the Connect-Tfs* cmdlets prior to invoking this cmdlet.");

                    break;
                }
                case string uri when Uri.IsWellFormedUriString(uri, UriKind.Absolute):
                {
                    connection = new Uri(uri);
                    continue;
                }
                default:
                {
                    throw new Exception($"Invalid or non-existent {scope} {connection}.");
                }
            }

            return (Connection) result;
        }

        [ImportingConstructor]
        public DataManagerImpl(
            [ImportMany] IEnumerable<Lazy<ICommand>> commands,
            IParameterManager parameterManager,
            ILogger logger,
            ICurrentConnections currentConnections)
        {
            Commands = commands;
            ParameterManager = parameterManager;
            Logger = logger;
            CurrentConnections = currentConnections;
        }
    }
}
