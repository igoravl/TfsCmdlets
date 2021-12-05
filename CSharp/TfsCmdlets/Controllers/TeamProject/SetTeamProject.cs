using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Cmdlets.TeamProject;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController]
    internal class SetTeamProjectController: ControllerBase<WebApiTeamProject>
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

            var client = Data.GetClient<ProjectHttpClient>(Parameters);
            var projectAvatar = new ProjectAvatar
            {
                Image = File.ReadAllBytes(avatarImage)
            };

            client.SetProjectAvatarAsync(projectAvatar, tp.Name)
                .Wait($"Error setting team project avatar");

            yield return tp;
        }

        [ImportingConstructor]
        public SetTeamProjectController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
          : base(powerShell, data, parameters, logger)
        {
        }
    }
}