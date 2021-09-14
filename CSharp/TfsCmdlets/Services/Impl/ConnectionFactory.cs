using System;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Util;
#if NET471_OR_GREATER
using Microsoft.TeamFoundation.Client;
#endif

namespace TfsCmdlets.Services.Impl
{
    [Exports(typeof(Models.Connection))]
    internal class ConnectionFactory : IFactory
    {
        private ClientScope _scope;
        private IParameterManager ParameterManager { get; set; }
        private ILogger Logger { get; set; }
        private ICurrentConnections CurrentConnections { get; set; }
        private IController<VssCredentials> CredentialsController { get; }

        public ConnectionFactory(
            IParameterManager parameterManager,
            ILogger logger,
            ICurrentConnections currentConnections,
            IController<VssCredentials> credentialsController)
        {
            ParameterManager = parameterManager;
            Logger = logger;
            CurrentConnections = currentConnections;
            CredentialsController = credentialsController;
        }

        public object Create()
        {
            object result = null;
            bool forceExit = false;

            var parms = ParameterManager.Get();

            var connection = parms.Get<object>(_scope.ToString());
            var current = parms.Get<bool>("Current");

            while (result == null && !forceExit) switch (connection)
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
                            Logger.Log($"Get {_scope} referenced by URL '{uri}'");
                            result = new VssConnection(uri, CredentialsController.NewItem(new { Url = uri }));
                            break;
                        }
#endif
#if NET471_OR_GREATER
                    case TfsConnection conn:
                        {
                            result = conn;
                            break;
                        }
                    case Uri uri when _scope == ClientScope.Server:
                        {
                            Logger.Log($"Get {_scope} referenced by URL '{uri}'");
                            result = new TfsConfigurationServer(uri, CredentialsController.NewItem(new{Url=uri}));
                            break;
                        }
                    case Uri uri when _scope == ClientScope.Collection:
                        {
                            Logger.Log($"Get {_scope} referenced by URL '{uri}'");
                            result = new TfsTeamProjectCollection(uri, CredentialsController.NewItem(new{Url=uri}));
                            break;
                        }
#endif
                    case null:
                        {
                            Logger.Log($"Get currently connected {_scope}");
                            result = ((Connection)CurrentConnections.Get(_scope.ToString()))?.InnerConnection;

                            if (result == null && current) return null;

                            ErrorUtil.ThrowIfNull(result, _scope.ToString(), $"No TFS connection information available. Either supply a valid -{_scope} argument or use one of the Connect-Tfs* cmdlets prior to invoking this cmdlet.");

                            break;
                        }
                    case string uri when Uri.IsWellFormedUriString(uri, UriKind.Absolute):
                        {
                            connection = new Uri(uri);
                            continue;
                        }
                    default:
                        {
                            throw new Exception($"Invalid or non-existent {_scope} {connection}.");
                        }
                }

            if (_scope == ClientScope.Server)
            {
                return new ConfigurationServer(result);
            }

            return new TeamProjectCollection(result);
        }

        public void SetContext(InjectAttribute injectAttribute)
        {
            if (injectAttribute is InjectConnectionAttribute attr)
            {
                _scope = attr.Scope;
                return;
            }

            throw new ArgumentException($"{nameof(injectAttribute)} is not a {nameof(InjectConnectionAttribute)}");
        }
    }
}
