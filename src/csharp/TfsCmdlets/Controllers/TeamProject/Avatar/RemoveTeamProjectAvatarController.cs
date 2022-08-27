using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.TeamProject.Avatar
{
    [CmdletController]
    partial class RemoveTeamProjectAvatarController
    {
        protected override IEnumerable Run()
        {
            var client = Data.GetClient<ProjectHttpClient>();

            foreach (var tp in Data.GetItems<WebApiTeamProject>(new { Project = Parameters.Get<object>(nameof(Project)) }))
            {
                if (!PowerShell.ShouldProcess($"[Project: {tp.Name}]", $"Remove custom team project avatar")) continue;

                Logger.Log($"Resetting team project avatar image to default");

                client.RemoveProjectAvatarAsync(tp.Name)
                    .Wait("Error removing project avatar");
            }
            return null;
        }
    }
}