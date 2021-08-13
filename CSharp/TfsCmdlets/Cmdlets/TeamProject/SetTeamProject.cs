using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using System.IO;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Changes the details of a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "TfsTeamProject")]
    [OutputType(typeof(WebApiTeamProject))]
    public class SetTeamProject : SetCmdletBase<WebApiTeamProject>
    {
        /// <summary>
        /// Specifies the name of the Team Project. 
        /// </summary>
        [Parameter(Position = 0)]
        public new object Project { get; set; }

        /// <summary>
        /// Specifies the name of a local image file to be uploaded and used as the team project icon ("avatar"). 
        /// To remove a previously set image, pass $null to this argument.
        /// </summary>
        [Parameter()]
        public string AvatarImage { get; set; }
    }

    partial class TeamProjectDataService
    {
        protected override WebApiTeamProject DoSetItem()
        {
            var (tpc, tp) = GetCollectionAndProject();

            var avatarImage = GetParameter<string>(nameof(SetTeamProject.AvatarImage));

            if(HasParameter(nameof(SetTeamProject.AvatarImage)))
            {
                var client = GetClient<ProjectHttpClient>();
                var projectAvatar = new ProjectAvatar();

                if(!string.IsNullOrEmpty(avatarImage))
                {
                    if(!File.Exists(avatarImage))
                    {
                        throw new ArgumentException($"Invalid avatar image path '{avatarImage}'");
                    }

                    projectAvatar.Image = File.ReadAllBytes(avatarImage);
                }

                client.SetProjectAvatarAsync(projectAvatar, tp.Name)
                    .Wait($"Error setting team project avatar");
            }

            return tp;
        }
    }
}