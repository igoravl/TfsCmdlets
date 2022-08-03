namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class ConnectTeamProjectController
    {
        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();

            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp);

            Logger.Log($"Connected to '{tp.Name}'");

            yield return tp;
        }

        [Import]
        private ICurrentConnections CurrentConnections { get; }
    }
}