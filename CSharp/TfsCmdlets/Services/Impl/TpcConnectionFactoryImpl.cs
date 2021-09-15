using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Services.Impl
{
    [Exports(typeof(Models.TpcConnection))]
    internal class TpcConnectionFactoryImpl : ConnectionFactoryBase
    {
        public TpcConnectionFactoryImpl(IParameterManager parameterManager,
            ILogger logger,
            ICurrentConnections currentConnections)
            : base(parameterManager, logger, currentConnections)
        {
        }

        protected override ClientScope Scope => ClientScope.Collection;
    }
}