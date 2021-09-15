// TODO

//using System.Collections.Generic;
//using TfsCmdlets.Models;
//using TfsCmdlets.Services;

//namespace TfsCmdlets.Cmdlets.ConfigServer
//{
//    [Controller(typeof(Models.ServerConnection))]
//    internal class ConfigurationServerController : SimpleControllerBase<Models.ServerConnection>
//    {
//        public ICurrentConnections CurrentConnections { get; private set; }
//        public ServerConnection Server { get; }

//        public ConfigurationServerController(
//            ICurrentConnections currentConnections,
//            Models.ServerConnection server,
//            ILogger logger,
//            IParameterManager parameterManager,
//            IPowerShellService powerShell)
//            : base(logger, parameterManager, powerShell)
//        {
//            CurrentConnections = currentConnections;
//            Server = server;
//        }

//        protected override IEnumerable<Models.ServerConnection> DoGetItems(ParameterDictionary parameters)
//        {
//            var current = parameters.Get<bool>("Current");

//            if (current)
//            {
//                yield return CurrentConnections.Server;
//                yield break;
//            }

//            yield return Server;
//        }

//        protected override ServerConnection DoConnectItem(ParameterDictionary parameters)
//        {
//            Server.Connect();

//            CurrentConnections.Set(Server);

//            Logger.Log(
//                $"Connected to {Server.Uri}, ID {Server.ServerId}, as '{Server.AuthorizedIdentity.DisplayName}'");

//            return Server;
//        }
//    }
//}