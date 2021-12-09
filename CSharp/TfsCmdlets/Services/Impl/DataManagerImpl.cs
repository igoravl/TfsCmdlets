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
using System.Collections;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IDataManager)), Shared]
    // [Export(typeof(IControllerManager))]
    internal class DataManagerImpl : IDataManager //, IControllerManager
    {
        protected IEnumerable<Lazy<IController>> Commands { get; }
        protected IParameterManager Parameters { get; }
        protected ILogger Logger { get; }
        protected ICurrentConnections CurrentConnections { get; }

        public IEnumerable<T> Invoke<T>(string verb, object overridingParameters = null)
        {
            var dataType = typeof(T);
            IController controller = Commands.FirstOrDefault(c => c.Value.Verb == verb && c.Value.DataType == dataType)?.Value as ITypedController<T>;

            if (controller == null)
            {
                throw new ArgumentException($"Command '{verb}' not found for data type '{dataType.Name}'");
            }

            return DoInvokeCommand<T>(controller, overridingParameters);
        }

        public IEnumerable<T> Invoke<T>(string verb, string noun, object overridingParameters = null)
        {
            IController controller = Commands.FirstOrDefault(c => c.Value.Verb == verb && c.Value.Noun == noun)?.Value as ITypedController<T>;

            if (controller == null)
            {
                throw new ArgumentException($"Command '{verb}{noun}' not found");
            }

            return DoInvokeCommand<T>(controller, overridingParameters);
        }

        public T GetItem<T>(object overridingParameters = null)
            => GetItems<T>(overridingParameters).First();

        public IEnumerable<T> GetItems<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Get, overridingParameters);

        public bool TestItem<T>(object overridingParameters = null)
        {
            try { return GetItems<T>(overridingParameters).Any(); }
            catch { return false; };
        }

        public T NewItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.New, overridingParameters).FirstOrDefault();

        public T SetItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Set, overridingParameters).FirstOrDefault();

        public void RemoveItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Remove, overridingParameters);

        public T RenameItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Rename, overridingParameters).FirstOrDefault();

        public Connection GetServer(object overridingParameters = null)
            => CreateConnection(ClientScope.Server, overridingParameters);

        public Connection GetCollection(object overridingParameters = null)
            => CreateConnection(ClientScope.Collection, overridingParameters);

        public WebApiTeamProject GetProject(object overridingParameters = null, string contextValue = null)
        {
            return GetItem<WebApiTeamProject>(overridingParameters);
        }

        public WebApiTeam GetTeam(object overridingParameters = null, string contextValue = null)
        {
            return GetItem<WebApiTeam>(overridingParameters);
        }

        public T GetClient<T>(object overridingParameters = null)
            => (T)((ITfsServiceProvider)GetCollection(overridingParameters)).GetClient(typeof(T));

        public T GetService<T>(object overridingParameters = null)
            => (T)((ITfsServiceProvider)GetCollection(overridingParameters)).GetService(typeof(T));

        private Connection CreateConnection(ClientScope scope, object overridingParameters = null)
        {
#if NETCOREAPP3_1_OR_GREATER
            Microsoft.VisualStudio.Services.WebApi.VssConnection result = null;
#else
            Microsoft.TeamFoundation.Client.TfsConnection result = null;
#endif
            var connection = Parameters.Get<object>(scope.ToString());
            var current = Parameters.Get<bool>("Current");

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

            return (Connection)result;
        }

        private IEnumerable<T> DoInvokeCommand<T>(IController controller, object overridingParameters)
        {
            Parameters.PushContext(overridingParameters);

            var result = controller.InvokeCommand();

            return new EnumerableWrapper<T>(result is IEnumerable<T> list ? list : new[] { (T)result }, () => Parameters.PopContext());
        }

        private class EnumerableWrapper<T> : IEnumerable<T>, IEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;
            private readonly Action _onEnumerationCompleted;
            private bool disposedValue;

            public T Current => _inner.Current;

            object IEnumerator.Current => Current;

            public EnumerableWrapper(IEnumerable<T> inner, Action onEnumerationCompleted)
            {
                _inner = inner.GetEnumerator();
                _onEnumerationCompleted = onEnumerationCompleted;
            }

            public IEnumerator<T> GetEnumerator() => this;

            public void Reset() => _inner.Reset();

            IEnumerator IEnumerable.GetEnumerator() => this;

            public bool MoveNext() => _inner.MoveNext();

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        _onEnumerationCompleted();
                    }
                    disposedValue = true;
                }
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }

        [ImportingConstructor]
        public DataManagerImpl(
            [ImportMany] IEnumerable<Lazy<IController>> commands,
            IParameterManager parameters,
            ILogger logger,
            ICurrentConnections currentConnections)
        {
            Commands = commands;
            Parameters = parameters;
            Logger = logger;
            CurrentConnections = currentConnections;
        }
    }
}
