using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    /// <summary>
    /// Removes the team project avatar, resetting it to the default.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class RemoveTeamProjectAvatar
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public object Project { get; set; }
    }

    [CmdletController(Client=typeof(IProjectHttpClient))]
    partial class RemoveTeamProjectAvatarController
    {
        protected override IEnumerable Run()
        {
            foreach (var tp in Data.GetItems<WebApiTeamProject>(new { Project = Parameters.Get<object>(nameof(Project)) }))
            {
                if (!PowerShell.ShouldProcess($"[Project: {tp.Name}]", $"Remove custom team project avatar")) continue;

                Logger.Log($"Resetting team project avatar image to default");

                Client.RemoveProjectAvatarAsync(tp.Name)
                    .Wait("Error removing project avatar");
            }
            return null;
        }
    }
}