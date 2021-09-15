using System;
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
    internal abstract class ConnectionFactoryBase : IFactory
    {
        private IParameterManager ParameterManager { get; set; }
        private ILogger Logger { get; set; }
        private ICurrentConnections CurrentConnections { get; set; }

        protected abstract ClientScope Scope { get; }

        public ConnectionFactoryBase(
            IParameterManager parameterManager,
            ILogger logger,
            ICurrentConnections currentConnections
            )
        {
            ParameterManager = parameterManager;
            Logger = logger;
            CurrentConnections = currentConnections;
        }

        public object Create()
        {
            object result = null;

            var parms = ParameterManager.GetParameters();

            var connection = parms.Get<object>(Scope.ToString());
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
                    Logger.Log($"Get {Scope} referenced by URL '{uri}'");
                    //result = new VssConnection(uri, CredentialsController.NewItem(new { Url = uri }));
                    break;
                }
#endif
#if NET471_OR_GREATER
                case Microsoft.TeamFoundation.Client.TfsConnection conn:
                    {
                        result = conn;
                        break;
                    }
                case Uri uri when Scope == ClientScope.Server:
                    {
                        Logger.Log($"Get {Scope} referenced by URL '{uri}'");
                        //result = new TfsConfigurationServer(uri, CredentialsController.NewItem(new{Url=uri}));
                        break;
                    }
                case Uri uri when Scope == ClientScope.Collection:
                    {
                        Logger.Log($"Get {Scope} referenced by URL '{uri}'");
                        //result = new TfsTeamProjectCollection(uri, CredentialsController.NewItem(new{Url=uri}));
                        break;
                    }
#endif
                case null:
                {
                    Logger.Log($"Get currently connected {Scope}");
                    result = ((Connection)CurrentConnections.Get(Scope.ToString()))?.InnerConnection;

                    if (result == null && current) return null;

                    ErrorUtil.ThrowIfNull(result, Scope.ToString(), $"No TFS connection information available. Either supply a valid -{Scope} argument or use one of the Connect-Tfs* cmdlets prior to invoking this cmdlet.");

                    break;
                }
                case string uri when Uri.IsWellFormedUriString(uri, UriKind.Absolute):
                {
                    connection = new Uri(uri);
                    continue;
                }
                default:
                {
                    throw new Exception($"Invalid or non-existent {Scope} {connection}.");
                }
            }

            if (Scope == ClientScope.Server)
            {
                return new ServerConnection(result);
            }

            return new TpcConnection(result);
        }

        public void SetContext(InjectAttribute injectAttribute)
        {
        }
    }
}
