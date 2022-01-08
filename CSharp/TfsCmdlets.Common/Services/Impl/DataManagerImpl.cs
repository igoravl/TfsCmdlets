using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Util;
using Microsoft.VisualStudio.Services.Common;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

#if NETCOREAPP3_1_OR_GREATER
using AdoConnection = Microsoft.VisualStudio.Services.WebApi.VssConnection;
#else
using AdoConnection = Microsoft.TeamFoundation.Client.TfsConnection;
using Microsoft.TeamFoundation.Client;
#endif

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IDataManager)), Shared]
    public class DataManagerImpl : IDataManager
    {
        private IEnumerable<Lazy<IController>> Commands { get; }
        private IParameterManager Parameters { get; }
        private ILogger Logger { get; }
        private ICurrentConnections CurrentConnections { get; }

        public IEnumerable<T> Invoke<T>(string verb, object overridingParameters = null)
        {
            var dataType = typeof(T);
            IController controller = Commands.FirstOrDefault(c => c.Value.Verb == verb && c.Value.DataType == dataType)?.Value as ITypedController<T>;

            if (controller == null)
            {
                throw new ArgumentException($"Command '{verb}' not found for data type '{dataType.Name}'");
            }

            foreach(var item in DoInvokeCommand(controller, overridingParameters))
            {
                yield return (T) item;
            }
        }

        public IEnumerable Invoke(string verb, string noun, object overridingParameters = null)
        {
            if (Commands.FirstOrDefault(c => c.Value.Verb == verb && c.Value.Noun == noun)?.Value is not { } controller)
            {
                throw new ArgumentException($"Controller '{verb}{noun}[Controller]' not found");
            }

            return DoInvokeCommand(controller, overridingParameters);
        }

        public T GetItem<T>(object overridingParameters = null)
            => GetItems<T>(overridingParameters).First();

        public IEnumerable<T> GetItems<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Get, overridingParameters);

        public bool TestItem<T>(object overridingParameters = null)
        {
            try { return GetItems<T>(overridingParameters).Any(); }
            catch { return false; }
        }

        public T NewItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.New, overridingParameters).FirstOrDefault();

        public T AddItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Add, overridingParameters).FirstOrDefault();

        public T SetItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Set, overridingParameters).FirstOrDefault();

        public void RemoveItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Remove, overridingParameters);

        public T RenameItem<T>(object overridingParameters = null)
            => Invoke<T>(VerbsCommon.Rename, overridingParameters).FirstOrDefault();

        public Connection GetServer(object overridingParameters = null)
            => CreateConnection(ClientScope.Server, overridingParameters) ??
                throw new ArgumentException("No server information available. Either supply a valid -Server argument or use Connect-TfsConfigurationServer prior to invoking this cmdlet.");

        public Connection GetCollection(object overridingParameters = null)
            => CreateConnection(ClientScope.Collection, overridingParameters) ??
                throw new ArgumentException("No team project collection information available. Either supply a valid -Collection argument or use Connect-TfsTeamProjectCollection prior to invoking this cmdlet.");

        public WebApiTeamProject GetProject(object overridingParameters = null, string contextValue = null)
            => GetItem<WebApiTeamProject>(overridingParameters) ??
                throw new ArgumentException("No team project information available. Either supply a valid -Project argument or use Connect-TfsTeamProject prior to invoking this cmdlet.");

        public Models.Team GetTeam(object overridingParameters = null, string contextValue = null)
            => GetItem<Models.Team>(overridingParameters) ??
                throw new ArgumentException("No team information available. Either supply a valid -Team argument or use Connect-TfsTeam prior to invoking this cmdlet.");

        public T GetClient<T>(object overridingParameters = null)
        {
            var conn = ((ITfsServiceProvider)GetCollection(overridingParameters));

            Logger.Log($"GetClient: Getting an instance of [{typeof(T).FullName}]");
            return (T)conn.GetClient(typeof(T));
        }

        public T GetService<T>(object overridingParameters = null)
        {
            var conn = ((ITfsServiceProvider)GetCollection(overridingParameters));

            Logger.Log($"GetService: Getting an instance of [{typeof(T).FullName}]");
            return (T)conn.GetService(typeof(T));
        }

        private Connection CreateConnection(ClientScope scope, object overridingParameters = null)
        {
            using (Parameters.PushContext(overridingParameters))
            {
                AdoConnection result;
                var connection = Parameters.Get<object>(scope.ToString());
                var current = Parameters.Get<bool>("Current");

                if (connection is string u && Uri.IsWellFormedUriString(u, UriKind.Absolute))
                {
                    connection = new Uri(u);
                }

                switch (connection)
                {
                    case Connection conn:
                        {
                            Logger.Log($"Returning the {scope} passed as an argument, unmodified.");
                            result = conn.InnerObject;
                            break;
                        }
#if NETCOREAPP3_1_OR_GREATER
                    case Microsoft.VisualStudio.Services.WebApi.VssConnection conn:
                        {
                            Logger.Log($"Returning the {scope} passed as an argument, unmodified.");
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
                    case Microsoft.VisualStudio.Services.WebApi.VssConnection conn when scope == ClientScope.Collection:
                        {
                            Logger.Log($"Get TfsTeamProjectCollection from the supplied VssConnection");
                            result = new TfsTeamProjectCollection(conn.Uri, conn.Credentials);
                            break;
                        }
                    case Microsoft.VisualStudio.Services.WebApi.VssConnection conn when scope == ClientScope.Server:
                        {
                            Logger.Log($"Get TfsConfigurationServer from the supplied VssConnection");
                            result = new TfsConfigurationServer(conn.Uri, conn.Credentials);
                            break;
                        }
                    case TfsTeamProjectCollection conn when scope == ClientScope.Collection:
                        {
                            Logger.Log($"Returning the {scope} passed as an argument, unmodified.");
                            result = conn;
                            break;
                        }
                    case TfsConfigurationServer conn when scope == ClientScope.Server:
                        {
                            Logger.Log($"Returning the {scope} passed as an argument, unmodified.");
                            result = conn;
                            break;
                        }
                    case Uri uri when scope == ClientScope.Server:
                        {
                            Logger.Log($"Get {scope} referenced by URL '{uri}'");
                            result = TfsConfigurationServerFactory.GetConfigurationServer(
                                uri, GetItem<VssCredentials>(new { Url = uri }));
                            break;
                        }
                    case Uri uri when scope == ClientScope.Collection:
                        {
                            Logger.Log($"Get {scope} referenced by URL '{uri}'");
                            result = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(
                                uri, GetItem<VssCredentials>(new { Url = uri }));
                            break;
                        }
#endif
                    case null:
                        {
                            Logger.Log($"Get currently connected {scope}");
                            result = ((Connection)CurrentConnections.Get(scope.ToString()))?.InnerObject;

                            break;
                        }
                    default:
                        {
                            throw new Exception($"Invalid or non-existent {scope} {connection}.");
                        }
                }

                return result;
            }
        }

        private IEnumerable DoInvokeCommand(IController controller, object overridingParameters)
        {
            Parameters.PushContext(overridingParameters);

            var result = controller.InvokeCommand();

            return new EnumerableWrapper(result is IEnumerable list ? list : new[] { result }, () => Parameters.PopContext());
        }

        private sealed class EnumerableWrapper : IEnumerable, IEnumerator
        {
            private readonly IEnumerator _inner;
            private readonly Action _onEnumerationCompleted;
            private bool _isDisposed;

            public object Current => _inner.Current;

            object IEnumerator.Current => Current;

            public EnumerableWrapper(IEnumerable inner, Action onEnumerationCompleted)
            {
                _inner = inner.GetEnumerator();
                _onEnumerationCompleted = onEnumerationCompleted;
            }

            public IEnumerator GetEnumerator() => this;

            public void Reset() => _inner.Reset();

            IEnumerator IEnumerable.GetEnumerator() => this;

            public bool MoveNext() => _inner.MoveNext();

            private void Dispose(bool disposing)
            {
                if (_isDisposed) return;

                if (disposing) _onEnumerationCompleted();

                _isDisposed = true;
            }

            public void Dispose()
            {
                Dispose(disposing: true);
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