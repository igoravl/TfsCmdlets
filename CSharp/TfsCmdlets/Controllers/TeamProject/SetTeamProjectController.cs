using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.TeamProject;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class SetTeamProjectController
    {
        public override IEnumerable<WebApiTeamProject> Invoke()
        {
            var tp = Data.GetProject();
            var avatarImage = Parameters.Get<string>(nameof(SetTeamProject.AvatarImage));

            if (string.IsNullOrEmpty(avatarImage) ||
                !PowerShell.ShouldProcess(tp, $"Set avatar image to {avatarImage}"))
            {
                yield return tp;
            }

            if (!File.Exists(avatarImage)) throw new ArgumentException($"Invalid avatar image path '{avatarImage}'");

            var client = Data.GetClient<ProjectHttpClient>();
            var projectAvatar = new ProjectAvatar
            {
                Image = File.ReadAllBytes(avatarImage)
            };

            client.SetProjectAvatarAsync(projectAvatar, tp.Name)
                .Wait();

            yield return tp;
        }
    }
}