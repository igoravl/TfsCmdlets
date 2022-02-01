using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.Team;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(Models.Team))]
    partial class RenameTeamController
    {
        protected override IEnumerable Run()
        {
            var client = Data.GetClient<TeamHttpClient>();

            var team = Data.GetTeam(new { Default = false });

            if (!PowerShell.ShouldProcess(Project, $"Rename team '{team.Name}' to '{NewName}'")) yield break;

            yield return client.UpdateTeamAsync(new WebApiTeam() { Name = NewName }, Project.Id.ToString(), team.Id.ToString())
                .GetResult($"Error renaming team '{team.Name}' to '{NewName}'");
        }
    }
}