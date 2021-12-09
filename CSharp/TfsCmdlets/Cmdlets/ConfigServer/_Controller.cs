// TODO

//using System.Collections.Generic;
//using TfsCmdlets.Models;
//using TfsCmdlets.Services;

//namespace TfsCmdlets.Cmdlets.ConfigServer
//{
//    [Controller(typeof(Models.Connection))]
//    internal class ConfigurationServerController : SimpleControllerBase<Models.Connection>
//    {
//        public ICurrentConnections CurrentConnections { get; private set; }
//        public Connection Server { get; }

//        public ConfigurationServerController(
//            ICurrentConnections currentConnections,
//            Models.Connection server,
//            ILogger logger,
//            IParameterManager parameterManager,
//            IPowerShellService powerShell)
//            : base(logger, parameterManager, powerShell)
//        {
//            CurrentConnections = currentConnections;
//            Server = server;
//        }

//        protected override IEnumerable<Models.Connection> Invoke(ParameterDictionary parameters)
//        {
//            var current = parameters.Get<bool>("Current");

//            if (current)
//            {
//                yield return CurrentConnections.Server;
//                yield break;
//            }

//            yield return Server;
//        }

//        protected override Connection DoConnectItem(ParameterDictionary parameters)
//        {
//            Server.Connect();

//            CurrentConnections.Set(Server);

//            Logger.Log(
//                $"Connected to {Server.Uri}, ID {Server.ServerId}, as '{Server.AuthorizedIdentity.DisplayName}'");

//            return Server;
//        }
//    }
//}