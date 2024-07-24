using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Changes the details of a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiTeamProject))]
    partial class SetTeamProject
    {
        /// <summary>
        /// Specifies the name of the Team Project. 
        /// </summary>
        [Parameter(Position = 0)]
        public object Project { get; set; }

        /// <summary>
        /// Specifies the description of the team project.
        /// To remove a previously set description, pass a blank string ('') to this argument.
        /// </summary>
        [Parameter]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the name of a local image file to be uploaded and used as the team project icon ("avatar"). 
        /// To remove a previously set image, pass either $null or a blank string ('') to this argument.
        /// </summary>
        [Parameter]
        public string AvatarImage { get; set; }
    }

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

                var result = AsyncAwaiter.Wait(client.UpdateProject(tp.Id, tpInfo), "Error updating team project description");

                if (result.Status != OperationStatus.Succeeded)
                {
                    Logger.LogError(new Exception($"Error updating team project '{tp.Name}': {result.ResultMessage}"));
                }
            }

            yield return GetItem<WebApiTeamProject>();
        }
    }
}