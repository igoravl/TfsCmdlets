using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Services.Impl
{
    [Exports(typeof(Models.ServerConnection))]
    internal class ServerConnectionFactoryImpl : ConnectionFactoryBase
    {
        public ServerConnectionFactoryImpl(IParameterManager parameterManager,
            ILogger logger,
            ICurrentConnections currentConnections)
            : base(parameterManager, logger, currentConnections)
        {
        }

        protected override ClientScope Scope => ClientScope.Server;
    }
}