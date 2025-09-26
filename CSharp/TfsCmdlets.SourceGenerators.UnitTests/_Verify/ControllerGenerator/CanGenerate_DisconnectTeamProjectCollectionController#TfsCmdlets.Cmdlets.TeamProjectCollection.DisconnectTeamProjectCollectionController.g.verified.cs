//HintName: TfsCmdlets.Cmdlets.TeamProjectCollection.DisconnectTeamProjectCollectionController.g.cs
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    internal partial class DisconnectTeamProjectCollectionController: ControllerBase
    {
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.Connection);
        protected override void CacheParameters()
        {
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public DisconnectTeamProjectCollectionController(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}