using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Cmdlets.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class SetTeamProjectController
    {
        [Import]
        private IAsyncOperationAwaiter AsyncAwaiter { get; }

        protected override IEnumerable Run()
        {
            if (Has_AvatarImage)
            {
                Logger.LogWarn($"The -AvatarImage parameter is deprecated and will be removed in a future version. Use the Import-TfsTeamProjectAvatar and Remove-TfsTeamProjectAvatar cmdlets instead.");

                if (string.IsNullOrEmpty(AvatarImage))
                {
                    Data.Invoke(VerbsCommon.Remove, "TeamProjectAvatar");
                }
                else
                {
                    Data.Invoke(VerbsData.Import, "TeamProjectAvatar", new
                    {
                        Path = AvatarImage
                    });
                }
            }

            if (Has_Description)
            {
                var client = GetClient<ProjectHttpClient>();
                var tp = Data.GetProject();
                var tpInfo = new WebApiTeamProject() { Description = Description };

                var (result, resultMessage) = AsyncAwaiter.Wait(client.UpdateProject(tp.Id, tpInfo), "Error updating team project description");

                if (result != OperationStatus.Succeeded)
                {
                    Logger.LogError(new Exception($"Error updating team project '{tp.Name}': {resultMessage}"));
                }
            }

            yield return GetItem<WebApiTeamProject>();
        }
    }
}
