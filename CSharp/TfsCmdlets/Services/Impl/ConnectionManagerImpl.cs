using System;
using System.Composition;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Util;
#if NET471_OR_GREATER
using Microsoft.TeamFoundation.Client;
#endif

// TODO

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IConnectionManager)), Shared]
    internal class ConnectionManagerImpl : IConnectionManager
    {
        private IDataManager Data { get; }
        private IParameterManager ParameterManager { get; set; }
        private ILogger Logger { get; set; }
        private ICurrentConnections CurrentConnections { get; set; }

        public TpcConnection GetCollection() => Create<TpcConnection>(ClientScope.Collection);

        public ServerConnection GetServer() => Create<ServerConnection>(ClientScope.Server);

        public T Create<T>(ClientScope scope)
            where T: Models.Connection
        {
            object result = null;

            var parms = ParameterManager.GetParameters();

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
                            result = new VssConnection(uri, Data.GetItem<VssCredentials>(new { Url = uri }));
                            break;
                        }
#endif
#if NET471_OR_GREATER
                case Microsoft.TeamFoundation.Client.TfsConnection conn:
                    {
                        result = conn;
                        break;
                    }
                case Uri uri when scope == ClientScope.Server:
                    {
                        Logger.Log($"Get {scope} referenced by URL '{uri}'");
                        result = new TfsConfigurationServer(uri, Data.GetItem<VssCredentials>(new { Url = uri }));
                        break;
                    }
                case Uri uri when scope == ClientScope.Collection:
                    {
                        Logger.Log($"Get {scope} referenced by URL '{uri}'");
                        result = new TfsTeamProjectCollection(uri, Data.GetItem<VssCredentials>(new { Url = uri }));
                        break;
                    }
#endif
                    case null:
                        {
                            Logger.Log($"Get currently connected {scope}");
                            result = ((Connection)CurrentConnections.Get(scope.ToString()))?.InnerConnection;

                            if (result == null && current) return null;

                            ErrorUtil.ThrowIfNull(result, scope.ToString(), $"No TFS connection information available. Either supply a valid -{scope} argument or use one of the Connect-Tfs* cmdlets prior to invoking this cmdlet.");

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

            if (scope == ClientScope.Server)
            {
                return new ServerConnection(result) as T;
            }

            return new TpcConnection(result) as T;
        }

        [ImportingConstructor]
        public ConnectionManagerImpl(
            IDataManager data,
            IParameterManager parameterManager,
            ILogger logger,
            ICurrentConnections currentConnections
            )
        {
            Data = data;
            ParameterManager = parameterManager;
            Logger = logger;
            CurrentConnections = currentConnections;
        }
    }
}
