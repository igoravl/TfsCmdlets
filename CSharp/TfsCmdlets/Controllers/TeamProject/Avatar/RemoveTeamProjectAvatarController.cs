using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.TeamProject.Avatar
{
    [CmdletController]
    partial class RemoveTeamProjectAvatarController
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var client = Data.GetClient<ProjectHttpClient>();

            if (PowerShell.ShouldProcess(tp, $"Reset custom team project avatar image to default"))
            {
                Logger.Log($"Resetting team project avatar image to default");

                client.RemoveProjectAvatarAsync(tp.Name)
                    .Wait("Error removing project avatar");
            }

            return null;
        }
    }
}