namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(Models.Team))]
    partial class DisconnectTeamController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                CurrentConnections.Collection,
                CurrentConnections.Project
            );

            return null;
        }
    }
}