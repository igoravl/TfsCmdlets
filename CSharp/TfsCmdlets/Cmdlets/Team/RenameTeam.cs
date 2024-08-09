using Microsoft.TeamFoundation.Core.WebApi;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Renames a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTeam))]
    partial class RenameTeam 
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public object Team { get; set; }
    }

    [CmdletController(typeof(Models.Team), Client=typeof(ITeamHttpClient))]
    partial class RenameTeamController
    {
        protected override IEnumerable Run()
        {
            var team = Data.GetItem<Models.Team>();

            if (!PowerShell.ShouldProcess(Project, $"Rename team '{team.Name}' to '{NewName}'")) yield break;

            yield return Client.UpdateTeamAsync(new WebApiTeam() { Name = NewName }, Project.Id.ToString(), team.Id.ToString())
                .GetResult($"Error renaming team '{team.Name}' to '{NewName}'");
        }
    }
}