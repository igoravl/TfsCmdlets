using System.Management.Automation;
using TfsCmdlets.Cmdlets.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class SetTeamProjectController
    {
        protected override IEnumerable Run()
        {
            if(!string.IsNullOrEmpty(AvatarImage))
            {
                Logger.LogWarn($"The -AvatarImage parameter is deprecated and will be removed in a future version. Use the Import-TfsTeamProjectAvatar cmdlet instead.");

                Data.Invoke(VerbsData.Import, "TeamProjectAvatar", new {
                    Path = AvatarImage
                });
            }

            return null;
        }
    }
}
