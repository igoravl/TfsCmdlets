using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.Team;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(WebApiTeam))]
    partial class RenameTeamController
    {
        public override IEnumerable<WebApiTeam> Invoke()
        {
            var tp = Data.GetProject();
            var t = Data.GetItem<WebApiTeam>();
            var newName = Parameters.Get<string>(nameof(RenameTeam.NewName));

            if (!PowerShell.ShouldProcess(tp, $"Rename team '{t.Name}' to '{newName}'")) yield break;

            var client = Data.GetClient<TeamHttpClient>();

            yield return client.UpdateTeamAsync(new WebApiTeam()
            {
                Name = newName
            }, tp.Id.ToString(), t.Id.ToString())
                .GetResult($"Error renaming team '{t.Name}' to '{newName}'");
        }
    }
}