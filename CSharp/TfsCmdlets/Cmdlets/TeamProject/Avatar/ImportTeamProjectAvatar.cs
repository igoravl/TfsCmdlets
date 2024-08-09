using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    /// <summary>
    /// Imports and sets a new team project avatar (image).
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, HostedOnly = true)]
    partial class ImportTeamProjectAvatar
    {
        /// <summary>
        /// Specifies the path of the image file to import. 
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Path { get; set; }
    }

    [CmdletController(Client=typeof(IProjectHttpClient))]
    partial class ImportTeamProjectAvatarController
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var path = Parameters.Get<string>(nameof(ImportTeamProjectAvatar.Path));

            if (string.IsNullOrEmpty(path) || !PowerShell.ShouldProcess(tp, $"Import and use '{path}' as team project avatar image"))
            {
                return null;
            }

            if (!File.Exists(path)){ throw new ArgumentException($"Invalid avatar image path '{path}'");}

            var projectAvatar = new ProjectAvatar
            {
                Image = File.ReadAllBytes(path)
            };

            Logger.Log($"Importing '{path}' and using it as avatar image for team project '{tp.Name}'");

            Client.SetProjectAvatarAsync(projectAvatar, tp.Name)
                .Wait("Error import team project avatar image");

            return null;
        }
    }
}