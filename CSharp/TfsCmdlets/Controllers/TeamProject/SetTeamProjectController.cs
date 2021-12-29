using TfsCmdlets.Cmdlets.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class SetTeamProjectController
    {
        public override IEnumerable<WebApiTeamProject> Invoke()
        {
            var tp = Data.GetProject();
            var avatarImage = Parameters.Get<string>(nameof(SetTeamProject.AvatarImage));

            if(!string.IsNullOrEmpty(avatarImage))
            {
                Logger.LogWarn($"The -AvatarImage parameter is deprecated and will be removed in a future version. Use the Import-TfsTeamProjectAvatar cmdlet instead.");

                Data.Invoke<object>("Import", "TeamProjectAvatar", new {
                    Path = avatarImage
                });
            }

            return null;
        }
    }
}
