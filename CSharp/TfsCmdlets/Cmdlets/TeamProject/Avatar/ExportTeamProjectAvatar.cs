using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    /// <summary>
    /// Exports the current avatar (image) of the specified team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, HostedOnly = true)]
    partial class ExportTeamProjectAvatar
    {
        /// <summary>
        /// Specifies the path of the file where the avatar image will be saved. 
        /// When omitted, the image will be saved to the current directory as &lt;team-project-name&gt;.png.
        /// </summary>
        [Parameter(Position = 0)]
        public string Path { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_OVERWRITE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }

    }

    [CmdletController]
    partial class ExportTeamProjectAvatarController
    {
        [Import]
        private IRestApiService RestApiService{get;set;}

        protected override IEnumerable Run()
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