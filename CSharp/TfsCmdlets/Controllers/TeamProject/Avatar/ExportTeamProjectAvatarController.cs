using TfsCmdlets.Cmdlets.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject.Avatar
{
    [CmdletController]
    partial class ExportTeamProjectAvatarController
    {
        [Import]
        private IRestApiService RestApiService{get;set;}

        public override object InvokeCommand()
        { 
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();
            var path = Parameters.Get<string>(nameof(ExportTeamProjectAvatar.Path), PowerShell.ResolvePath($"{tp.Name}.png"));
            var force = Parameters.Get<bool>(nameof(ExportTeamProjectAvatar.Force));

            if (!PowerShell.ShouldProcess(tp, $"Export team project avatar image to '{path}'"))
            {
                return null;
            }

            path = PowerShell.ResolvePath(path);

            if (!force && File.Exists(path)){ throw new ArgumentException($"File '{path}' already exists. To overwrite an existing file, use the -Force switch.");}

            var identity = Data.GetItem<Models.Identity>(new {
                Identity = $"[{tp.Name}]\\{tp.DefaultTeam.Name}"
            });

            var apiPath = $"_apis/graph/Subjects/{identity.SubjectDescriptor}/avatars";

            var imageData = RestApiService.InvokeAsync<string>(tpc, apiPath, "GET", apiVersion: "6.1", serviceHostName: "vssps.dev.azure.com")
                .GetResult("Error exporting team project avatar image");

            File.WriteAllBytes(path, Convert.FromBase64String(imageData));

            return null;
        }
    }
}
