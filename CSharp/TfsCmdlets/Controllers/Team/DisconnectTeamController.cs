namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(Models.Team))]
    partial class DisconnectTeamController
    {
        protected override IEnumerable Run()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                CurrentConnections.Collection,
                CurrentConnections.Project
            );

            return null;
        }

        [Import]
        private ICurrentConnections CurrentConnections { get; }
    }
}