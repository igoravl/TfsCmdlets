namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class DisconnectTeamProjectController
    {
        protected override IEnumerable Run()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                CurrentConnections.Collection,
                null
            );

            return null;
        }

        [Import]
        private ICurrentConnections CurrentConnections { get; }
    }
}