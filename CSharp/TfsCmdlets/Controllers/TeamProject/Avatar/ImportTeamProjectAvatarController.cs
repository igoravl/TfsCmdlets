using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject.Avatar
{
    [CmdletController]
    partial class ImportTeamProjectAvatarController
    {
        public override object InvokeCommand()
        {
            var tp = Data.GetProject();
            var path = Parameters.Get<string>(nameof(ImportTeamProjectAvatar.Path));

            if (string.IsNullOrEmpty(path) || !PowerShell.ShouldProcess(tp, $"Import and use '{path}' as team project avatar image"))
            {
                return null;
            }

            if (!File.Exists(path)){ throw new ArgumentException($"Invalid avatar image path '{path}'");}

            var client = Data.GetClient<ProjectHttpClient>();

            var projectAvatar = new ProjectAvatar
            {
                Image = File.ReadAllBytes(path)
            };

            Logger.Log($"Importing '{path}' and using it as avatar image for team project '{tp.Name}'");

            client.SetProjectAvatarAsync(projectAvatar, tp.Name)
                .Wait("Error import team project avatar image");

            return null;
        }
    }
}
