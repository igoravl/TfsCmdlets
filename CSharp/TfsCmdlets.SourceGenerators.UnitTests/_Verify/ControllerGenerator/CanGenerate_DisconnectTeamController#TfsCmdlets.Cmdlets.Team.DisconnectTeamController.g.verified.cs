//HintName: TfsCmdlets.Cmdlets.Team.DisconnectTeamController.g.cs
namespace TfsCmdlets.Cmdlets.Team
{
    internal partial class DisconnectTeamController: ControllerBase
    {
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.Team);
        protected override void CacheParameters()
        {
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public DisconnectTeamController(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}